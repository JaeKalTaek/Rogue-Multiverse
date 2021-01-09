using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SC_BasePlayerCharacter : SC_BaseCharacter {

    public static SC_BasePlayerCharacter Player;

    public enum Alignments { None, Superhero, Villain, AntiHero }

    public Alignments Alignment { get; set; }

    [Header ("Tweakable")]
    public float accelerationTime;
    public float accelerationStrength, decelerationStrength;
    public float moveSpeed;

    protected float horizontalAcceleration, verticalAcceleration;

    protected virtual Vector2 XMovement { get { return (Vector2.right * Input.GetAxis ("Horizontal") * (accelerationTime == 0 ? moveSpeed : Mathf.Lerp(0, moveSpeed, horizontalAcceleration / accelerationTime)) * Time.deltaTime); } }

    protected virtual Vector2 YMovement { get { return Vector2.up * Input.GetAxis ("Vertical") * (accelerationTime == 0 ? moveSpeed : Mathf.Lerp (0, moveSpeed, verticalAcceleration / accelerationTime)) * Time.deltaTime; } }

    public bool Paused { get; set; }

    protected SC_Checkpoint checkpoint;

    protected void Awake() {

        Player = this;        

    }

    protected virtual void Start () {

        if (Alignment != Alignments.None)
            GetComponent<SpriteRenderer>().color = Alignment == Alignments.Superhero ? Color.green : (Alignment == Alignments.Villain ? Color.red : Color.cyan);

    }

    protected virtual void Update() {

        if (CanInteract)
            GetOver<SC_InteractableElement>("Interactable")?.Interact();

        if (!Paused) {

            horizontalAcceleration = Mathf.Clamp(horizontalAcceleration + Time.deltaTime * (Input.GetAxis("Horizontal") != 0 ? accelerationStrength : decelerationStrength), 0, accelerationTime);

            verticalAcceleration = Mathf.Clamp (verticalAcceleration + Time.deltaTime * (Input.GetAxis ("Vertical") != 0 ? accelerationStrength : decelerationStrength), 0, accelerationTime);

            Move (XMovement);

            AdditionalMovement();

        }

        checkpoint = GetOver<SC_Checkpoint>("Checkpoint") ?? checkpoint;

    }

    protected void CompleteLevel () {

        Paused = true;

        SceneManager.LoadScene("HUB");

    }

    protected virtual Collider2D MovementCheck (Vector2 movement) {

        return Physics2D.BoxCast (transform.position, transform.lossyScale, 0, movement, movement.magnitude, LayerMask.GetMask ("Default")).collider;

    }

    protected virtual bool Move(Vector2 movement) {

        if (movement != Vector2.zero) {

            if (!MovementCheck(movement)) {

                transform.position += movement.V3 ();

                return false;

            } else
                return true;

        } else
            return false;

    }

    protected abstract void AdditionalMovement();

    public T GetOver<T>(string id) where T : Behaviour {

        return Physics2D.OverlapBox(transform.position, transform.lossyScale, 0, LayerMask.GetMask(id))?.GetComponent<T>();

    }

    public virtual bool CanInteract { get { return Input.GetButtonDown("Submit"); } }

}

