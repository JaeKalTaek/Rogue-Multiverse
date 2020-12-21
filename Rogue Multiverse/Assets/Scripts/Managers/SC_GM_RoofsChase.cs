using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;

public class SC_GM_RoofsChase : SC_GameManager {

    List<SC_RoofsChase_Roof> roofs;

    protected override void Start() {

        base.Start ();

        roofs = new List<SC_RoofsChase_Roof> (FindObjectsOfType<SC_RoofsChase_Roof> ());

    }

    void Update() {
        
        if (Player)
            foreach (SC_RoofsChase_Roof roof in roofs)
                roof.gameObject.layer = LayerMask.NameToLayer (roof.transform.position.z < Player.transform.position.z ? "Default" : "Ignore Raycast");

    }

}
