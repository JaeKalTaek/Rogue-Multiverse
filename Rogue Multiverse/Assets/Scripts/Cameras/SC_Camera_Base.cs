using UnityEngine;

public class SC_Camera_Base : MonoBehaviour {

    public static Camera Cam { get; set; }    

    void Awake () {

        Cam = GetComponent<Camera> ();

    }

}
