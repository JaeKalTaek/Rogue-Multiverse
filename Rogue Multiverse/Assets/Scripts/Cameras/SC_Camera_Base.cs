using UnityEngine;

public class SC_Camera_Base : MonoBehaviour {

    public static Camera Cam { get; set; }

    public float Width { get { return Cam.aspect * Cam.orthographicSize * 2f; } }

    void Awake () {

        Cam = GetComponent<Camera> ();

    }

}
