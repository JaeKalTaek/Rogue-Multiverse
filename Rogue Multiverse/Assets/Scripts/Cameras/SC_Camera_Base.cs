using UnityEngine;

public class SC_Camera_Base : MonoBehaviour {

    public Camera Cam { get; set; }

    public float Width { get { return Cam.aspect * Cam.orthographicSize * 2f; } }

    void Start () {

        Cam = GetComponent<Camera> ();

    }

}
