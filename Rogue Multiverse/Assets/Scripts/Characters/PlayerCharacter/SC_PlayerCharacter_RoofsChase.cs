using UnityEngine;
using static SC_GameManager;
using static SC_Camera_Base;

public class SC_PlayerCharacter_RoofsChase : SC_BasePlayerCharacter {

    public float gravity;

    public AnimationCurve jumpCurve;

    [Range(0f, 1f)]
    public float jumpControl;

    [Range(0f, 1f)]
    public float fallControl;

    public float deathHeight, spriteSizePerUnit;

    protected float jumpTime;
    float? jumpStart = null;

    protected float verticalAcceleration;

    Vector3 baseScale;

    float baseCamDistance;

    Collider2D under;

    protected void Start () {

        baseScale = transform.lossyScale;

        baseCamDistance = Vector2.Distance(transform.position, Cam.transform.position);

    }

    protected override void Update() {

        under = GetOver<Collider2D>("Ignore Raycast");

        base.Update();

    }

    float AirControl { get { return jumpStart != null ? jumpControl : ((!under || under.transform.position.z > transform.position.z) ? fallControl : 1); } }

    protected override Vector2 BaseMovement => base.BaseMovement * AirControl;

    protected override void AdditionalMovement () {

        verticalAcceleration += Mathf.Clamp (verticalAcceleration += Time.deltaTime * (Input.GetAxis ("Vertical") != 0 ? 1 : -1), 0, accelerationTime);

        Move (Vector2.up * Input.GetAxis ("Vertical") * Mathf.Lerp (0, moveSpeed, verticalAcceleration / accelerationTime) * Time.deltaTime * AirControl);        

        if (jumpStart == null) {

            if (!under || under.transform.position.z > transform.position.z) {

                transform.Set(null, null, Mathf.Min(under?.transform.position.z ?? deathHeight, transform.position.z + gravity * Time.deltaTime));

                if (transform.position.z >= deathHeight && !GM.Fail()) {

                    transform.position = checkpoint?.SpawnPos ?? GM.playerSpawnPoint.position;

                    Cam.GetComponent<SC_Camera_RoofsChase>().Respawned(baseCamDistance);

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

        transform.localScale = baseScale - Vector3.one * spriteSizePerUnit * transform.position.z;

    }

}
