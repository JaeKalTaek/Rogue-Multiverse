using UnityEngine;

public class SC_PlayerCharacter_HUB : SC_BasePlayerCharacter {

    [Header ("Tweakable")]
    public float gravity;
    public float airControl;
    public float jumpHeight, jumpDuration;

    public LayerMask floorLayerMask;

    float jumpStart, jumpTime;

    protected override void Start () {

        base.Start ();

        jumpTime = -1;

    }

    protected override void AdditionalMovement () {        

        bool grounded = Physics2D.Raycast (transform.position, Vector2.down, .57f, floorLayerMask);

        movement *= (grounded || (jumpTime >= 0) ? 1 : airControl);

        if (jumpTime < 0) {

            if (!grounded)
                movement += Vector2.down * gravity * Time.fixedDeltaTime;
            else if (Input.GetAxisRaw ("Vertical") > 0) {

                jumpTime = 0;

                jumpStart = transform.position.y;

            }

        }

        if (jumpTime >= 0) {

            jumpTime += Time.fixedDeltaTime;

            float lerp = jumpTime / jumpDuration;

            movement += Vector2.up * (Mathf.Lerp (jumpStart, jumpStart + jumpHeight, lerp) - transform.position.y);

            jumpTime = (lerp >= 1) || (Input.GetAxis ("Vertical") <= 0) ? -1 : jumpTime;

        }        

        RaycastHit2D target = Physics2D.Raycast (transform.position, movement, movement.magnitude + .5f);

        if (target.collider)
            jumpTime = -1;

    }    

}

