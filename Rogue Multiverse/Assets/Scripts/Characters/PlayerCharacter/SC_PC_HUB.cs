using UnityEngine;

public class SC_PC_HUB : SC_BasePlayerCharacter {

    [Header ("Tweakable")]
    public float gravity;
    public float airControl;
    public AnimationCurve jump;

    float jumpStart;
    float jumpTime = -1;

    bool Grounded { get { return Physics2D.Raycast(transform.position, Vector2.down, .57f, LayerMask.GetMask("Default")); } }

    protected override Vector2 XMovement => base.XMovement * (Grounded || (jumpTime >= 0) ? 1 : airControl);

    protected override void AdditionalMovement () {        

        Vector2 movement = Vector2.zero;

        if (jumpTime < 0) {

            if (!Grounded)
                movement += Vector2.down * gravity * Time.deltaTime;
            else if (Input.GetButtonDown ("Jump")) {

                jumpTime = 0;

                jumpStart = transform.position.y;

            }

        }

        if (jumpTime >= 0) {

            jumpTime = Mathf.Min (jump.Length (), jumpTime + Time.deltaTime);

            movement += Vector2.up * (jumpStart + jump.Evaluate (jumpTime) - transform.position.y);

            jumpTime = (jumpTime < jump.Length () && Input.GetButton ("Jump")) ? jumpTime : -1;

        }

        if (Move (movement))
            jumpTime = -1;

    }

    protected override void Update () {

        base.Update ();

        if (Grounded) {

            if (Input.GetButtonDown ("Submit"))
                GetOver<SC_InteractableElement> ("Interactable")?.Interact ();
            else if (!Paused && Input.GetButtonDown ("Teleport"))
                SC_Teleporter.teleporter.Teleport ();

        }

    }

}

