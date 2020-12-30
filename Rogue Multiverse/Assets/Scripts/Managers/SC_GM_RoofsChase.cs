using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;
using static SC_Camera_Base;

public class SC_GM_RoofsChase : SC_GameManager {

    [Header("Roofs Chase parameters")]
    public int checkpointPerRoof;

    [Header("Procedural generation parameters")]
    public int levelLength;

    public Vector2Int xDiffRange, yDiffRange;

    public AnimationCurve maxRoofSize;

    public int maxHeight;

    [Min(0.01f)]
    public float maxDistance;    

    List<SC_RoofsChase_Roof> roofs;

    protected override void Start() {

        base.Start ();

        roofs = new List<SC_RoofsChase_Roof>(FindObjectsOfType<SC_RoofsChase_Roof>());

        for (int i = 1; i < levelLength; i++) {

            Vector2 s = new Vector2(maxRoofSize.Evaluate (Random.value), maxRoofSize.Evaluate(Random.value));

            Vector3 p = roofs[roofs.Count - 1].transform.position + Vector3.right * Random.Range(xDiffRange.x, xDiffRange.y - 1) + Vector3.up * Random.Range (yDiffRange.x, yDiffRange.y - 1);

            p = new Vector3(Mathf.Clamp(p.x, (-Cam.aspect * Cam.orthographicSize) + 1, Cam.aspect * Cam.orthographicSize - 1), p.y, 0);

            float h = Mathf.Clamp(roofs[roofs.Count - 1].height + Random.Range(-1, 2), 1, maxHeight);

            RaycastHit2D t = Physics2D.BoxCast(p, s, 0, roofs[roofs.Count - 1].transform.position - p, 15f, LayerMask.GetMask("Ignore Raycast"));

            if (Physics2D.OverlapBox(p, s, 0, LayerMask.GetMask("Ignore Raycast"))?.GetComponent<SC_RoofsChase_Roof>() ||
                t.distance + h - roofs[roofs.Count - 1].height > maxDistance) {

                i--;

                continue;

            }

            print(t.distance + h - roofs[roofs.Count - 1].height);

            SC_RoofsChase_Roof r = Instantiate(Resources.Load<SC_RoofsChase_Roof>("Prefabs/LD/PF_Roof"));

            r.height = h;

            r.size = s;            

            r.transform.position = p;

            r.Setup();

            r.gameObject.SetActive(false);

            r.gameObject.SetActive(true);

            roofs.Add(r);

            if (i % checkpointPerRoof == 0) {

                SC_Checkpoint c = Instantiate(Resources.Load<SC_Checkpoint>("Prefabs/LD/PF_Checkpoint"));

                c.transform.position = Vector3.up * p.y;

                c.GetComponent<BoxCollider2D>().size = new Vector2(Cam.aspect * Cam.orthographicSize * 2, 1);

                c.transform.GetChild(0).transform.position += Vector3.right * p.x + Vector3.back * h;

            }

        }        

    }

    void Update() {
        
        if (Player)
            foreach (SC_RoofsChase_Roof roof in roofs)
                roof.gameObject.layer = LayerMask.NameToLayer (roof.transform.position.z < Player.transform.position.z ? "Default" : "Ignore Raycast");

    }

}
