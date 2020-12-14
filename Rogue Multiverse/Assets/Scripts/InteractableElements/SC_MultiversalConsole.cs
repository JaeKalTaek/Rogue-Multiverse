using UnityEngine;
using static SC_PlayerCharacter;

public class SC_MultiversalConsole : SC_InteractableElement {

    public GameObject map;

    public override void Interact () {        

        Player.Paused ^= true;

        map.SetActive (Player.Paused);

    }

}
