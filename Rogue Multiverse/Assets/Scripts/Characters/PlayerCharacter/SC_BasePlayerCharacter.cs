using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SC_BasePlayerCharacter : SC_BaseCharacter {

    public static SC_BasePlayerCharacter Player;

    public enum Alignments { None, Superhero, Villain, AntiHero }

    public Alignments Alignment { get; set; }

    [Header("Tweakable")]
    public float accelerationTime;
    public float moveSpeed;

    protected float horizontalAcceleration;

    protected virtual Vector2 BaseMovement { get { return (Vector2.right * Input.GetAxis("Horizontal") * Mathf.Lerp(0, moveSpeed, horizontalAcceleration / accelerationTime) * Time.deltaTime); } }

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

            horizontalAcceleration = Mathf.Clamp(horizontalAcceleration += Time.deltaTime * (Input.GetAxis("Horizontal") != 0 ? 1 : -1), 0, accelerationTime);

            Move(BaseMovement);

            AdditionalMovement();

        }

        checkpoint = GetOver<SC_Checkpoint>("Checkpoint") ?? checkpoint;

    }

    protected void CompleteLevel () {

        Paused = true;

        SceneManager.LoadScene("HUB");

    }

    protected virtual bool Move(Vector2 movement) {

        if (movement != Vector2.zero) {

            RaycastHit2D t = Physics2D.BoxCast(transform.position, transform.lossyScale, 0, movement, movement.magnitude, LayerMask.GetMask("Default"));

            if (!t.collider)
                transform.position += movement.V3();

            return t.collider;

        } else
            return false;

    }

    protected abstract void AdditionalMovement();

    public T GetOver<T>(string id) where T : Behaviour {

        return Physics2D.OverlapBox(transform.position, transform.lossyScale, 0, LayerMask.GetMask(id))?.GetComponent<T>();

    }

    public virtual bool CanInteract { get { return Input.GetButtonDown("Submit"); } }

}

