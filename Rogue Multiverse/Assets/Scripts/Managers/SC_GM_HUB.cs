using TMPro;
using UnityEngine;

public class SC_GM_HUB : SC_GameManager {

    public SC_MultiversalConsole console;

    [Header("HUB/Meta parameters")]
    public int[] progressSteps;

    public int CurrentProgress { get; set; }

    public int CurrentProgressStep { get; set; }

    public float Progress { get { return CurrentProgress / (float)progressSteps[CurrentProgressStep]; } }    

    protected override void Start() {

        if (GM) {

            tutorial.GetComponentInChildren<TextMeshProUGUI>().text = GM.HUBmessage;

            CurrentProgress += GM.HUBmessage.Contains("failed") ? 0 : 1;

            Destroy(GM.gameObject);

        } else
            tutorial = null;

        base.Start();        

    }

    public void Discover () {

        CurrentProgress -= progressSteps[CurrentProgressStep];

        CurrentProgressStep++;

        console.menu.transform.GetChild(0).GetChild(CurrentProgressStep).gameObject.SetActive(true);

        console.UpdateValues();

    }

}
