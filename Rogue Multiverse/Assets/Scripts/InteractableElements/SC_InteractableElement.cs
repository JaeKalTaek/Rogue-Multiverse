using UnityEngine;
using static SC_BasePlayerCharacter;

public abstract class SC_InteractableElement : MonoBehaviour {

    public GameObject menu;

    public virtual void Interact () {

        Player.Paused ^= true;

        menu.SetActive (Player.Paused);

    }

}
