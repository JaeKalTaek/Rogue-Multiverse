using TMPro;
using UnityEngine;
using static SC_SaveFile;

public class SC_GM_HUB : SC_GameManager {  

    protected override void Start() {

        if(save == null)
            SetSave ();

        if (GM) {

            tutorial.GetComponentInChildren<TextMeshProUGUI>().AddBefore (GM.HUBmessage + "\n");

            save.currentProgress += GM.HUBmessage.Contains("failed") ? 0 : 1;
            Save ();

            Destroy(GM.gameObject);

        }

        base.Start();        

    }    

    void Update () {

        if (Input.GetKeyDown (KeyCode.Escape)) {

            save = new SC_SaveFile ();
            Save ();

        }

    }

}
