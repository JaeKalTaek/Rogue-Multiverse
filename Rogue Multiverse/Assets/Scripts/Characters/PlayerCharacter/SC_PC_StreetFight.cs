using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PC_StreetFight : SC_BasePlayerCharacter {

    [Header ("Street Fight PC variables")]
    public Collider2D simplePunchCollider;
    public int simplePunchDamage;
    public float simplePunchSpeed;    

    protected override void Start () {

        base.Start ();

        animator.SetFloat ("SimplePunchSpeed", simplePunchSpeed);

    }

    protected override Collider2D MovementCheck (Vector2 movement) {

        return Physics2D.BoxCast (new Vector3 (ColliderPos.x, transform.position.y + .1f), new Vector2 (collider.bounds.size.x, .2f), 0, movement, movement.magnitude, LayerMask.GetMask ("Default")).collider ??
            Physics2D.BoxCast (new Vector3 (ColliderPos.x, transform.position.y + .1f), collider.bounds.size / 2, 0, movement, movement.magnitude, LayerMask.GetMask ("Enemy")).collider;

    }

    protected override void AdditionalMovement () {

        Move (YMovement);

    }

    IEnumerator punchCoroutine;

    protected override void Update () {

        Vector3 prevPos = transform.position;

        base.Update ();

        animator.SetBool ("Walking", Vector3.Distance (prevPos, transform.position) > 0);

        if (!Paused) {

            if (Input.GetButtonDown ("Submit")) {

                animator.SetTrigger ("SimplePunch");

                punchCoroutine = Punching ();
                StartCoroutine (punchCoroutine);

            }

        }

    }

    IEnumerator Punching () {

        List<Collider2D> hits = new List<Collider2D> ();

        while (true) {
             
            List<Collider2D> results = new List<Collider2D> ();
            simplePunchCollider.OverlapCollider (SC_ExtensionMethods.GetFilter ("EnemyHitbox"), results);

            foreach (Collider2D c in results) {

                if (!hits.Contains (c)) {

                    c.GetComponentInParent<SC_BaseCharacter> ()?.Hit (simplePunchDamage);

                    hits.Add (c);

                }

            }

            yield return new WaitForSeconds (Time.deltaTime);

        }

    }

    public void SimplePunched () {

        StopCoroutine (punchCoroutine);

    }

}