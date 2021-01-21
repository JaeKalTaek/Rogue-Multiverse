using UnityEngine;
using static SC_GameManager;
using static SC_Camera_Base;
using System.Collections;

public class SC_PC_RoofsChase : SC_BasePlayerCharacter {

    public float gravity;

    public AnimationCurve jumpCurve;

    [Range(0f, 1f)]
    public float jumpControl;

    [Range(0f, 1f)]
    public float fallControl;

    public float deathHeight, spriteSizePerUnit, respawnPause;

    public Vector2 shadowOffset;
    SpriteRenderer shadow;

    protected float jumpTime;
    float? jumpStart = null;

    Vector3 baseScale;

    float baseCamDistance;

    Collider2D under;

    protected override void Start () {

        base.Start();

        baseScale = transform.lossyScale;

        baseCamDistance = Vector2.Distance(transform.position, Cam.transform.position);

        shadow = new GameObject ("Shadow").AddComponent<SpriteRenderer> ();
        shadow.sprite = GetComponent<SpriteRenderer> ().sprite;
        shadow.color = new Color (0, 0, 0, .9f);
        shadow.transform.parent = transform;
        shadow.transform.localPosition = Vector3.zero;
        shadow.transform.localScale = Vector3.one;
    }

    protected override void Update() {        

        under = GetOver<Collider2D>("Ignore Raycast");

        if (!Paused && GetOver<Collider2D>("Checkpoint")?.name == "End" && AirControl == 1)
            CompleteLevel();        

        base.Update();

        transform.localScale = baseScale - Vector3.one * spriteSizePerUnit * transform.position.z;

        bool grounded = under?.transform.position.z == transform.position.z;

        shadow.sortingLayerName = grounded ? "Default" : "Player";
        shadow.sortingOrder = grounded ? 1 : 0;

        shadow.transform.localPosition = shadowOffset * (grounded ? 1 : ((under?.transform.position.z ?? deathHeight) - transform.position.z));

    }

    float AirControl { get { return jumpStart != null ? jumpControl : ((!under || under.transform.position.z > transform.position.z) ? fallControl : 1); } }

    protected override Vector2 XMovement => base.XMovement * AirControl;

    protected override void AdditionalMovement () {

        Move (YMovement * AirControl);        

        if (jumpStart == null) {

            if (!under || under.transform.position.z > transform.position.z) {

                transform.Set(null, null, Mathf.Min(under?.transform.position.z ?? deathHeight, transform.position.z + gravity * Time.deltaTime));

                if (transform.position.z >= deathHeight) {

                    GM.Fail ();

                    return;

                    /*transform.position = checkpoint?.SpawnPos ?? GM.playerSpawnPoint.position;

                    Cam.GetComponent<SC_Camera_RoofsChase>().Respawned(baseCamDistance);

                    Paused = true;

                    StartCoroutine (RespawnPause ());*/

                }

            } else if (Input.GetButtonDown("Jump")) {

                jumpTime = 0;

                jumpStart = transform.position.z;

            } 

        } else {

            jumpTime = Mathf.Min (jumpTime + Time.deltaTime, jumpCurve.Length ());

            transform.Set (null, null, jumpStart - jumpCurve.Evaluate (jumpTime));

            if (jumpTime >= jumpCurve.Length ())
                jumpStart = null;

        }

    }    

    /*IEnumerator RespawnPause () {

        yield return new WaitForSeconds (respawnPause);

        Paused = false;

    }*/

}
