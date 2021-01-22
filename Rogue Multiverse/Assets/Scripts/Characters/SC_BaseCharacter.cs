using UnityEngine;

public class SC_BaseCharacter : MonoBehaviour {

    [Header ("Base character variables")]
    public int baseHealth;
    public virtual int Health { get; set; }

    public float moveSpeed;

    protected Animator animator;

    protected SpriteRenderer spriteR;

    public Collider2D Collider { get; set; }

    public Vector3 ColliderPos { get { return transform.position + Vector3.Scale (Collider.offset, transform.lossyScale); } }

    protected virtual void Start () {

        animator = GetComponent<Animator> ();

        spriteR = GetComponent<SpriteRenderer> ();

        Collider = GetComponent<Collider2D> ();

        Health = baseHealth;

    }

    public void Hit (int damage) {

        Health -= damage;

        if (Health <= 0)
            Destroy (gameObject);

    }

}
