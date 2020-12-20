using UnityEngine;

public class SC_GameManager : MonoBehaviour {

    public static SC_GameManager GM;

    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    protected virtual void Start() {

        GM = this;

        Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        
    }

}
