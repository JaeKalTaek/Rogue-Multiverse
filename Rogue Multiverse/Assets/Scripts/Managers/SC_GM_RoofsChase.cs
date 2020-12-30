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

    public AnimationCurve maxRoofSize, xDiff;

    public int maxHeight;

    [Min(0.01f)]
    public float maxDistance;    

    List<SC_RoofsChase_Roof> roofs;

    protected override void Start() {

        base.Start ();

        roofs = new List<SC_RoofsChase_Roof>(FindObjectsOfType<SC_RoofsChase_Roof>());

        for (int i = 1; i < levelLength; i++) {

            Vector2 s = new Vector2(maxRoofSize.Evaluate (Random.value), maxRoofSize.Evaluate(Random.value));

            Vector3 p = roofs[roofs.Count - 1].transform.position + Vector3.right * (Random.value >= .5f ? -xDiff.Evaluate(Random.value) : xDiff.Evaluate(Random.value)) + Vector3.up * Random.Range (yDiffRange.x, yDiffRange.y - 1);

            p = new Vector3(Mathf.Clamp(p.x, (-Cam.aspect * Cam.orthographicSize) + 1, Cam.aspect * Cam.orthographicSize - 1), p.y, 0);

            float h = Mathf.Clamp(roofs[roofs.Count - 1].height + Random.Range(-1, 2), 1, maxHeight);

            RaycastHit2D t = Physics2D.BoxCast(p, s, 0, roofs[roofs.Count - 1].transform.position - p, 15f, LayerMask.GetMask("Ignore Raycast"));

            if (Physics2D.OverlapBox(p, s, 0, LayerMask.GetMask("Ignore Raycast"))?.GetComponent<SC_RoofsChase_Roof>() ||
                t.distance + h - roofs[roofs.Count - 1].height > maxDistance) {

                i--;

                continue;

            }

            //print(p.x - roofs[roofs.Count - 1].transform.position.x);

            //print(t.distance + h - roofs[roofs.Count - 1].height);

            AddRoof(h, s, p);

            roofs[roofs.Count - 1].gameObject.SetActive(false);

            roofs[roofs.Count - 1].gameObject.SetActive(true);

            if (i % checkpointPerRoof == 0) {

                SC_Checkpoint c = Instantiate(Resources.Load<SC_Checkpoint>("Prefabs/LD/PF_Checkpoint"));

                c.transform.position = Vector3.up * p.y;

                c.GetComponent<BoxCollider2D>().size = new Vector2(Cam.aspect * Cam.orthographicSize * 2, .1f);

                c.transform.GetChild(0).transform.position += Vector3.right * p.x + Vector3.back * h;

            }

        }

        AddRoof(7, new Vector2(19.2f, 5), new Vector3(0, roofs[roofs.Count - 1].transform.position.y + (roofs[roofs.Count - 1].size.y + 5) / 2 + 0.04f + (7 - roofs[roofs.Count - 1].height) * .4f, 0));

    }

    void AddRoof (float height, Vector2 size, Vector3 pos) {

        SC_RoofsChase_Roof r = Instantiate(Resources.Load<SC_RoofsChase_Roof>("Prefabs/LD/PF_Roof"));

        r.height = height;

        r.size = size;

        r.transform.position = pos;

        r.Setup();

        roofs.Add(r);

    }

    void Update() {
        
        if (Player)
            foreach (SC_RoofsChase_Roof roof in roofs)
                roof.gameObject.layer = LayerMask.NameToLayer (roof.transform.position.z < Player.transform.position.z ? "Default" : "Ignore Raycast");

    }

}
