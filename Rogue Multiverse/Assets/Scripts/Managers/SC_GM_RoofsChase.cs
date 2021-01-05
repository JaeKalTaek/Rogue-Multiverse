using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;
using static SC_Camera_Base;

public class SC_GM_RoofsChase : SC_GameManager {

    [Header("Roofs Chase parameters")]
    public int checkpointPerRoof;

    [Header("Procedural generation parameters")]
    public Vector2Int levelLength;

    public AnimationCurve maxRoofSize, xDiff;
    public float xMargin;
    public AnimationCurve yDiff;

    public int maxHeight;

    [Min(0.01f)]
    public float maxDistance;

    [Header("Final roof parameters")]
    public int finalRoofHeight;

    public float finalRoofLength;

    List<SC_RoofsChase_Roof> roofs;

    SC_RoofsChase_Roof LastRoof { get { return roofs[roofs.Count - 1]; } }

    [Header("Prefabs")]
    public GameObject roofPrefab;
    public GameObject checkpointPrefab;

    protected override void Start() {

        base.Start ();

        roofs = new List<SC_RoofsChase_Roof>(FindObjectsOfType<SC_RoofsChase_Roof>());

        for (int i = 1; i < Mathf.Max(1, Random.Range(levelLength.x, levelLength.y + 1)); i++) {

            Vector2 s = new Vector2(maxRoofSize.Evaluate (Random.value), maxRoofSize.Evaluate(Random.value));

            float xRange = Random.Range(-ClampedX(false), ClampedX(true));            

            Vector3 p = LastRoof.transform.position + Vector3.right * xDiff.Evaluate(Mathf.Abs(xRange)).B(xRange >= 0) + Vector3.up * yDiff.Evaluate(Random.value);

            float h = Mathf.Clamp(LastRoof.height + Random.Range(-1, 2), 1, maxHeight);

            RaycastHit2D t = Physics2D.BoxCast(p, s, 0, LastRoof.transform.position - p, 15f, LayerMask.GetMask("Ignore Raycast"));

            if (Physics2D.OverlapBox(p, s, 0, LayerMask.GetMask("Ignore Raycast"))?.GetComponent<SC_RoofsChase_Roof>() ||
                t.distance + h - LastRoof.height > maxDistance) {

                i--;

                continue;

            }

            AddRoof(h, s, p, "N°" + i);

            LastRoof.gameObject.SetActive(false);

            LastRoof.gameObject.SetActive(true);

            if (i % checkpointPerRoof == 0) {

                GameObject c = Instantiate (checkpointPrefab);

                c.transform.position = Vector3.up * p.y;

                c.GetComponent<BoxCollider2D>().size = new Vector2(Cam.Width(), .1f);

                c.transform.GetChild(0).transform.position += Vector3.right * p.x + Vector3.back * h;

            }

        }

        BoxCollider2D end = new GameObject("End").AddComponent<BoxCollider2D>();

        end.transform.position = LastRoof.transform.position;

        end.size = LastRoof.size / 2;

        end.gameObject.layer = LayerMask.NameToLayer("Checkpoint");

        AddRoof(finalRoofHeight, new Vector2(Cam.Width(), finalRoofLength), new Vector3(0, LastRoof.transform.position.y + (LastRoof.size.y + finalRoofLength) / 2 + 0.04f + (finalRoofHeight - LastRoof.height) * .4f, 0), "Last");

    }

    float ClampedX (bool right) {

        for (float x = 0; x <= 1; x += .01f)
            if (xDiff.Evaluate(x) > Mathf.Abs((Cam.HalfWidth() - xMargin).B(right) - LastRoof.transform.position.x))
                return Mathf.Max(0, x - .01f);

        return 1;

    }

    void AddRoof (float height, Vector2 size, Vector3 pos, string name) {

        SC_RoofsChase_Roof r = Instantiate (roofPrefab).GetComponent<SC_RoofsChase_Roof> ();

        r.height = height;

        r.size = size;

        r.transform.position = pos;

        r.Setup();

        r.name = "Roof " + name;

        roofs.Add(r);

    }

    void Update() {
        
        if (Player)
            foreach (SC_RoofsChase_Roof roof in roofs)
                roof.gameObject.layer = LayerMask.NameToLayer (roof.transform.position.z < Player.transform.position.z ? "Default" : "Ignore Raycast");

    }

}
