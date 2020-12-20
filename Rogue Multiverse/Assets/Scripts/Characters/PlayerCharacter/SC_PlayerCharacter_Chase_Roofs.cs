using UnityEngine;
using static SC_GameManager;

public class SC_PlayerCharacter_Chase_Roofs : SC_BasePlayerCharacter {

    public float gravity;

    public AnimationCurve jumpCurve;

    public float deathHeight, minSpriteSize;

    protected bool jumping;    

    protected float verticalAcceleration;

    protected override void AdditionalMovement () {

        verticalAcceleration += Mathf.Clamp (verticalAcceleration += Time.deltaTime * (Input.GetAxis ("Vertical") != 0 ? 1 : -1), 0, accelerationTime);

        Move (Vector2.up * Input.GetAxis ("Vertical") * Mathf.Lerp (0, moveSpeed, verticalAcceleration / accelerationTime) * Time.deltaTime);

        if (!Physics2D.OverlapBox (transform.position, Vector2.one, 0, LayerMask.GetMask ("Ignore Raycast"))) {

            transform.position += Vector3.forward * gravity * Time.deltaTime;            

            if (transform.position.z >= deathHeight) {

                transform.position = GM.playerSpawnPoint.position;

                transform.localScale = Vector3.one;

            } else
                transform.localScale = Vector3.one * Mathf.Lerp (1, minSpriteSize, Mathf.InverseLerp (0, deathHeight, transform.position.z));

        }

        /*if (array[0]) {

            if (Input.GetButtonDown ("Jump") && !jumping) {



            }

        } else {

            height = Mathf.Max (deathHeight, height - gravity * Time.fixedDeltaTime);

        }

        */

    }

}
