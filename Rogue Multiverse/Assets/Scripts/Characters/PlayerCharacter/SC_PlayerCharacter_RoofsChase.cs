using UnityEngine;
using static SC_GameManager;

public class SC_PlayerCharacter_RoofsChase : SC_BasePlayerCharacter {

    public float gravity;

    public AnimationCurve jumpCurve;

    public float deathHeight, spriteSizePerUnit;

    protected float jumpTime;
    float? jumpStart;

    protected float verticalAcceleration;

    protected override void Start () {

        base.Start ();

        jumpStart = null;

    }

    protected override void AdditionalMovement () {

        verticalAcceleration += Mathf.Clamp (verticalAcceleration += Time.deltaTime * (Input.GetAxis ("Vertical") != 0 ? 1 : -1), 0, accelerationTime);

        Move (Vector2.up * Input.GetAxis ("Vertical") * Mathf.Lerp (0, moveSpeed, verticalAcceleration / accelerationTime) * Time.deltaTime);

        Collider2D under = Physics2D.OverlapBox (transform.position, Vector2.one, 0, LayerMask.GetMask ("Ignore Raycast"));

        if (!under || under.transform.position.z > transform.position.z) {

            transform.position = transform.position.Copy (null, null, Mathf.Min (under?.transform.position.z ?? deathHeight, transform.position.z + gravity * Time.deltaTime));

            if (transform.position.z >= deathHeight && !GM.Fail ()) {

                transform.position = GM.playerSpawnPoint.position;

                transform.localScale = Vector3.one;

            }

        } else if (Input.GetButtonDown ("Jump") && jumpStart == null) {

            jumpTime = 0;

            jumpStart = transform.position.z;

        }

        if (jumpStart != null) {

            jumpTime = Mathf.Min (jumpTime + Time.deltaTime, jumpCurve.Length ());

            transform.position = transform.position.Copy (null, null, jumpStart - jumpCurve.Evaluate (jumpTime));

            if (jumpTime >= jumpCurve.Length ())
                jumpStart = null;

        }

        transform.localScale = Vector3.one - Vector3.one * spriteSizePerUnit * transform.position.z;

    }

}
