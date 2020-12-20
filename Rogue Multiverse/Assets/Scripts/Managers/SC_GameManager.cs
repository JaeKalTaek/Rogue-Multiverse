using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GameManager : MonoBehaviour {

    public static SC_GameManager GM;

    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    public int lives;

    public TextMeshProUGUI livesText;

    protected virtual void Start() {

        GM = this;

        Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        if (lives > 0)
            livesText.text = "Lives: " + lives;
        
    }

    public bool Fail () {

        lives--;

        if (lives < 0)
            SceneManager.LoadScene ("HUB");
        else
            livesText.text = "Lives: " + lives;

        return lives < 0;

    }

}
