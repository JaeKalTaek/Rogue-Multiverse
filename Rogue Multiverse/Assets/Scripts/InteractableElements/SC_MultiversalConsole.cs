using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SC_BasePlayerCharacter;
using static SC_GameManager;

public class SC_MultiversalConsole : SC_InteractableElement {

    public Slider progressBar;

    public TextMeshProUGUI progressText;

    public Button discoveryButton;

    public GameObject discoveryCompleted;

    SC_GM_HUB G { get { return GM as SC_GM_HUB; } }

    public override void Interact() {

        if (!Player.Paused)
            UpdateValues();

        base.Interact();

    }


    public void UpdateValues () {

        if (G.CurrentProgressStep == G.progressSteps.Length - 1) {

            progressBar.transform.parent.gameObject.SetActive(false);

            discoveryCompleted.SetActive(true);

        } else {

            progressBar.value = Mathf.Clamp01(G.Progress);

            progressText.text = G.CurrentProgress + "/" + G.progressSteps[G.CurrentProgressStep];

            discoveryButton.interactable = G.Progress >= 1;

        }

    }

}
