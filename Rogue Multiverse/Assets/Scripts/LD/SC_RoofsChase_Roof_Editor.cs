using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SC_RoofsChase_Roof))]
[CanEditMultipleObjects]
public class SC_RoofsChase_Roof_Editor : Editor {

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        if (GUILayout.Button("Update visuals"))
            foreach (SC_RoofsChase_Roof r in targets)
                r.Setup();

    }

}
