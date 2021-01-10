using UnityEngine;

public class SC_PC_StreetFight : SC_BasePlayerCharacter {

    protected override Collider2D MovementCheck (Vector2 movement) {

        return Physics2D.BoxCast (transform.position + Vector3.up * .05f, new Vector2 (transform.lossyScale.x, .1f), 0, movement, movement.magnitude, LayerMask.GetMask ("Default")).collider ??
            Physics2D.BoxCast (transform.position + Vector3.up * transform.lossyScale.y * .25f, transform.lossyScale * .5f, 0, movement, movement.magnitude, LayerMask.GetMask ("Enemy")).collider;

    }

    protected override void AdditionalMovement () {

        Move (YMovement);

    }

}