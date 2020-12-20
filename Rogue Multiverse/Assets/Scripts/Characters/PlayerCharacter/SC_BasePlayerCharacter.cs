using UnityEngine;

public abstract class SC_BasePlayerCharacter : SC_BaseCharacter {

    public static SC_BasePlayerCharacter Player;

    [Header ("Tweakable")]
    public float accelerationTime;
    public float moveSpeed;

    protected float horizontalAcceleration;

    protected bool Grounded { get { return Physics2D.Raycast (transform.position, Vector2.down, .57f, LayerMask.GetMask ("Default")); } }

    protected virtual Vector2 BaseMovement { get { return (Vector2.right * Input.GetAxis ("Horizontal") * Mathf.Lerp (0, moveSpeed, horizontalAcceleration / accelerationTime) * Time.deltaTime); } }

    public bool Paused { get; set; }

    protected virtual void Start () {

        Player = this;

    }

    protected virtual void Update () {

        if (Input.GetButtonDown ("Submit") && Grounded)
            Physics2D.OverlapBox (transform.position, Vector2.one, 0, LayerMask.GetMask ("Interactable"))?.GetComponent<SC_InteractableElement> ().Interact ();

        if (!Paused) {

            horizontalAcceleration = Mathf.Clamp (horizontalAcceleration += Time.deltaTime * (Input.GetAxis ("Horizontal") != 0 ? 1 : -1), 0, accelerationTime);

            Move (BaseMovement);    

            AdditionalMovement ();           

        }

    }    

    protected virtual bool Move (Vector2 movement) {

        if (movement != Vector2.zero) {

            RaycastHit2D t = Physics2D.BoxCast (transform.position, Vector2.one, 0, movement, movement.magnitude, LayerMask.GetMask ("Default"));

            if (!t.collider)
                transform.position += movement.V3 ();

            return t.collider;

        } else
            return false;

    }

    protected abstract void AdditionalMovement ();

}

