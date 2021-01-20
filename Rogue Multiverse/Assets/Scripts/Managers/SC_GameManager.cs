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

    public GameObject tutorial;
    public float tutorialDuration;

    protected virtual void Start() {

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

        if (GetType () != typeof (SC_GM_HUB))
            DontDestroyOnLoad(this);

    }

    IEnumerator TutorialDuration () {

        yield return new WaitForSeconds(tutorialDuration);

        tutorial.SetActive(false);

        Player.Paused = false;
        
    }

    public void Fail () {

        HUBmessage = "You failed the scenario";

        SceneManager.LoadScene("HUB");

    }    

}
