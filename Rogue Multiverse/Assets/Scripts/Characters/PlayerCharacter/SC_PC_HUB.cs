using UnityEngine;

public class SC_PC_HUB : SC_BasePlayerCharacter {

    [Header ("Tweakable")]
    public float gravity;
    public float airControl;
    public float jumpHeight, jumpDuration;

    float jumpStart;
    float jumpTime = -1;

    bool Grounded { get { return Physics2D.Raycast(transform.position, Vector2.down, .57f, LayerMask.GetMask("Default")); } }

    public override bool CanInteract => base.CanInteract && Grounded;

    protected override Vector2 XMovement => base.XMovement * (Grounded || (jumpTime >= 0) ? 1 : airControl);

    protected override void AdditionalMovement () {        

        Vector2 movement = Vector2.zero;

        if (jumpTime < 0) {

            if (!Grounded)
                movement += Vector2.down * gravity * Time.deltaTime;
            else if (Input.GetAxisRaw ("Vertical") > 0) {

                jumpTime = 0;

                jumpStart = transform.position.y;

            }

        }

        if (jumpTime >= 0) {

            jumpTime += Time.deltaTime;

            float lerp = jumpTime / jumpDuration;

            movement += Vector2.up * (Mathf.Lerp (jumpStart, jumpStart + jumpHeight, lerp) - transform.position.y);

            jumpTime = (lerp >= 1) || (Input.GetAxis ("Vertical") <= 0) ? -1 : jumpTime;

        }

        if (Move (movement))
            jumpTime = -1;

    }

}

