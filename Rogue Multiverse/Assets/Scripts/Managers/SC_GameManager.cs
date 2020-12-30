using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SC_BasePlayerCharacter;

public class SC_GameManager : MonoBehaviour {    

    public static SC_GameManager GM;

    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    [Serializable]
    public struct AlignmentTutorial {

        public Alignments alignment;

        public string tutorial;

    }

    public List<AlignmentTutorial> alignmentTutorials;

    public int baseLives;
    int lives;

    public TextMeshProUGUI livesText;

    public GameObject tutorial;

    protected virtual void Start() {

        lives = baseLives;

        GM = this;

        SC_PlayerCharacter_RoofsChase p = Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent <SC_PlayerCharacter_RoofsChase> ();

        if (alignmentTutorials.Count > 0) {

            int a = UnityEngine.Random.Range(0, alignmentTutorials.Count);

            p.Alignment = alignmentTutorials[a].alignment;

            if (tutorial)
                tutorial.GetComponentInChildren<TextMeshProUGUI>().text = alignmentTutorials[a].tutorial + "\n" + tutorial.GetComponentInChildren<TextMeshProUGUI>().text;

        }

        if (tutorial) {

            p.Paused = true;

            tutorial.SetActive(true);

            StartCoroutine(TutorialDuration ());

        }

        if (lives > 0)
            livesText.text = "Lives: " + baseLives;      

    }

    IEnumerator TutorialDuration () {

        yield return new WaitForSeconds(1);

        tutorial.SetActive(false);

        Player.Paused = false;
        
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
