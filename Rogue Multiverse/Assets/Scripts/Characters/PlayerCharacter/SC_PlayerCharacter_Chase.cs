using UnityEngine;

public class SC_PlayerCharacter_Chase : SC_BasePlayerCharacter {

    protected override void AdditionalMovement () {

        movement += Vector2.up * Input.GetAxis ("Vertical") * Mathf.Lerp (0, moveSpeed, currentAcceleration / accelerationTime) * Time.fixedDeltaTime;

    }

}
