using UnityEngine;

public class SC_GameManager : MonoBehaviour {

    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    void Start() {

        Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        
    }

}
