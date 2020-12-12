using UnityEngine;

public class SC_GameManager : MonoBehaviour {

    public Transform playerSpawnPoint;

    void Start() {

        Instantiate (Resources.Load ("Prefabs/Characters/P_PlayerCharacter"), playerSpawnPoint.position, Quaternion.identity);
        
    }

}
