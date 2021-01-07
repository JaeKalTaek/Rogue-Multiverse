using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SC_BasePlayerCharacter;

public class SC_GameManager : MonoBehaviour {    

    public static SC_GameManager GM;

    [Header("Base GM parameters")]
    public Transform playerSpawnPoint;

    public GameObject playerPrefab;

    [Serializable]
    public struct AlignmentTexts {

        public Alignments alignment;

        public string tutorial, successMessage;

    }

    public List<AlignmentTexts> alignmentTexts;

    public string HUBmessage { get; set; }

    public int baseLives;
    int lives;

    public TextMeshProUGUI livesText;

    public GameObject tutorial;
    public float tutorialDuration;

    protected virtual void Start() {

        lives = baseLives;

        GM = this;

        SC_BasePlayerCharacter p = Instantiate (playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent <SC_BasePlayerCharacter> ();

        if (alignmentTexts.Count > 0) {

            int a = UnityEngine.Random.Range(0, alignmentTexts.Count);

            p.Alignment = alignmentTexts[a].alignment;

            tutorial?.GetComponentInChildren<TextMeshProUGUI>().AddBefore(alignmentTexts[a].tutorial + "\n");

            HUBmessage = alignmentTexts[a].successMessage;

        }

        if (tutorial) {

            p.Paused = true;

            tutorial.SetActive(true);

            StartCoroutine(TutorialDuration ());

        }

        if (lives > 0) {

            DontDestroyOnLoad(this);

            livesText.text = "Lives: " + baseLives;

        }

    }

    IEnumerator TutorialDuration () {

        yield return new WaitForSeconds(tutorialDuration);

        tutorial.SetActive(false);

        Player.Paused = false;
        
    }

    public bool Fail () {

        lives--;

        if (lives < 0) {

            HUBmessage = "You failed the scenario";

            SceneManager.LoadScene("HUB");

        } else
            livesText.text = "Lives: " + lives;

        return lives < 0;

    }    

}
