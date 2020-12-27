using UnityEditor;

[CustomEditor(typeof(SC_RoofsChase_Roof))]
[CanEditMultipleObjects]
public class SC_RoofsChase_Roof_Editor : Editor {

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        foreach (SC_RoofsChase_Roof t in targets)
                t.UpdateVisuals();

    }

}
