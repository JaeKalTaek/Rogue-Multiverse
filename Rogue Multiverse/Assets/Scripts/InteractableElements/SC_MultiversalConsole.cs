using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SC_BasePlayerCharacter;
using static SC_SaveFile;

public class SC_MultiversalConsole : SC_InteractableElement {

    [Header ("Progress parameters")]
    public int[] progressSteps;

    [Header("UI")]
    public Slider progressBar;

    public TextMeshProUGUI progressText;

    public Button discoveryButton;

    public GameObject discoveryCompleted;

    public override void Interact() {

        if (!Player.Paused)
            UpdateValues();

        base.Interact();

    }

    public void Discover () {

        save.currentProgress -= progressSteps[save.currentProgressStep];

        save.currentProgressStep++;

        UpdateValues ();

    }

    public void UpdateValues () {

        for (int i = 0; i <= save.currentProgressStep; i++)
            menu.transform.GetChild (0).GetChild (i).gameObject.SetActive (true);

        if (save.currentProgressStep == progressSteps.Length - 1) {

            progressBar.transform.parent.gameObject.SetActive(false);

            discoveryCompleted.SetActive(true);

        } else {

            progressBar.value = Mathf.Clamp01(save.currentProgress / (float) progressSteps[save.currentProgressStep]);

            progressText.text = save.currentProgress + "/" + progressSteps[save.currentProgressStep];

            discoveryButton.interactable = save.currentProgress / (float) progressSteps[save.currentProgressStep] >= 1;

        }

    }

}
