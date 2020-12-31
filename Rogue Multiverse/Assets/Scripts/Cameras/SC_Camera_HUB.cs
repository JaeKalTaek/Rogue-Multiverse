using UnityEngine;
using static SC_BasePlayerCharacter;

public class SC_Camera_HUB : SC_Camera_Base {

    void Update () {

        if (Player) {

            if (Cam.WorldToScreenPoint (Player.transform.position).x > Screen.width) {

                Cam.transform.position += Vector3.right * Cam.Width();

            } else if (Cam.WorldToScreenPoint (Player.transform.position).x < 0) {

                Cam.transform.position -= Vector3.right * Cam.Width();

            }

        }

    }

}
