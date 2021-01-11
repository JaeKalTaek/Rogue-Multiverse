using System.Collections;
using UnityEngine;

public class SC_PC_StreetFight : SC_BasePlayerCharacter {

    [Header ("Street Fight PC variables")]
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

        base.Update ();

        if (!Paused) {

            if (Input.GetButtonDown ("Submit")) {

                animator.SetTrigger ("SimplePunch");

                punchCoroutine = Punching ();
                StartCoroutine (punchCoroutine);

            }

        }

    }

    IEnumerator Punching () {

        while (true) {

            yield return new WaitForSeconds (Time.deltaTime);

            print ("PUNCHING");

        }

    }

    public void SimplePunched () {

        print ("PUNCHED");

        StopCoroutine (punchCoroutine);

    }

}