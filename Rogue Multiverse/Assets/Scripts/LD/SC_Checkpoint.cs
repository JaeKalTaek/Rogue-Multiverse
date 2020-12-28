using UnityEngine;

public class SC_Checkpoint : MonoBehaviour {
    
    public Vector3 SpawnPos { get { return transform.GetChild(0).transform.position; } }

}
