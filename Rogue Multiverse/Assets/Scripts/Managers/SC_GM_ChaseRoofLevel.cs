using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;

public class SC_GM_ChaseRoofLevel : SC_GameManager {

    List<SC_ChaseRoofBuilding> roofs;

    protected override void Start() {

        base.Start ();

        roofs = new List<SC_ChaseRoofBuilding> (FindObjectsOfType<SC_ChaseRoofBuilding> ());

    }

    void Update() {
        
        foreach (SC_ChaseRoofBuilding roof in roofs)
            roof.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer (roof.transform.position.z < Player.transform.position.z ? "Default" : "Ignore Raycast");

    }

}
