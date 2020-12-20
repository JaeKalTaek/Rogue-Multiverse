using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GameManager : MonoBehaviour {

    public static SC_GameManager GM;

    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    public int baseLives;
    int lives;

    public TextMeshProUGUI livesText;

    protected virtual void Start() {

        lives = baseLives;

        GM = this;

        Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        if (lives > 0)
            livesText.text = "Lives: " + baseLives;
        
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
