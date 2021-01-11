using UnityEngine;

public class SC_BaseCharacter : MonoBehaviour {

    [Header ("Base character variables")]
    public float baseHealth;
    public float Health { get; set; }

    protected Animator animator;

    protected SpriteRenderer spriteR;

    protected new Collider2D collider;

    public Vector3 ColliderPos { get { return transform.position + Vector3.Scale (collider.offset, transform.lossyScale); } }

    protected virtual void Start () {

        animator = GetComponent<Animator> ();

        spriteR = GetComponent<SpriteRenderer> ();

        collider = GetComponent<Collider2D> ();

        Health = baseHealth;

    }

}
