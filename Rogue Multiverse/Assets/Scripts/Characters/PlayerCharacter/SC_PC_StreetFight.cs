using UnityEngine;

public class SC_PC_StreetFight : SC_BasePlayerCharacter {

    protected override RaycastHit2D MovementCheck (Vector2 movement) {

        return Physics2D.BoxCast (transform.position + Vector3.down * .5f, new Vector2 (transform.lossyScale.x, .01f), 0, movement, movement.magnitude, LayerMask.GetMask ("Default"));

    }

    protected override void AdditionalMovement () {

        Move (YMovement);

    }

}