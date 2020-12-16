using UnityEngine;

public abstract class SC_BasePlayerCharacter : SC_BaseCharacter {

    public static SC_BasePlayerCharacter Player;

    [Header ("Tweakable")]
    public float accelerationTime;
    public float moveSpeed;

    protected float currentAcceleration;

    protected Vector2 movement;

    protected SC_InteractableElement interactableElement;
    
    public bool Paused { get; set; }

    protected virtual void Start () {

        Player = this;

    }

    protected void Update () {

        if (Input.GetButtonDown ("Submit"))
            interactableElement?.Interact ();

    }

    protected virtual void FixedUpdate () {

        if (!Paused) {

            currentAcceleration = Mathf.Clamp (currentAcceleration += Time.fixedDeltaTime * (Input.GetAxis ("Horizontal") != 0 ? 1 : -1), 0, accelerationTime);

            movement = Vector2.right * Input.GetAxis ("Horizontal") * Mathf.Lerp (0, moveSpeed, currentAcceleration / accelerationTime) * Time.fixedDeltaTime;
                      
            AdditionalMovement ();

            GetComponent<Rigidbody2D> ().MovePosition (transform.position.V2 () + movement);

        }

    }

    protected abstract void AdditionalMovement ();

    protected void OnTriggerEnter2D (Collider2D collision) {

        interactableElement = collision.GetComponent<SC_InteractableElement> ();

    }

    protected void OnTriggerExit2D (Collider2D collision) {

        if (interactableElement?.gameObject == collision.gameObject)
            interactableElement = null;
        
    }

}

