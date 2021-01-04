﻿using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static SC_BasePlayerCharacter;

public class SC_Teleporter : SC_InteractableElement {

    public string[] levels;

    bool close;

    public override void Interact () {

        if (!Player.Paused) {

            base.Interact ();

            menu.transform.GetChild (1).GetComponent<Button> ().Select ();

        } else if (close) {

            close = false;

            base.Interact ();            

        }

    }

    public void Close () {

        close = true;

    }

    public void Teleport () {

        SC_SaveFile.Save ();

        SceneManager.LoadScene (levels[Random.Range(0, SC_SaveFile.save.currentProgressStep + 1)]);

    }

}
