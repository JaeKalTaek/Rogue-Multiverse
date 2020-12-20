using UnityEngine;

public class SC_PlayerCharacter_Chase_Roofs : SC_BasePlayerCharacter {

    public float gravity;

    public AnimationCurve jumpCurve;

    public float deathHeight, minSpriteSize;

    protected bool jumping;    

    protected float secondAcceleration;

    protected float height;

    protected Collider2D movementCollider;

    protected override void Start () {

        base.Start ();

        movementCollider = transform.GetChild (0).GetComponent<Collider2D> ();

    }

    protected override void AdditionalMovement () {

        secondAcceleration += Mathf.Clamp (secondAcceleration += Time.fixedDeltaTime * (Input.GetAxis ("Vertical") != 0 ? 1 : -1), 0, accelerationTime);

        Vector2 movement = Vector2.up * Input.GetAxis ("Vertical") * Mathf.Lerp (0, moveSpeed, secondAcceleration / accelerationTime) * Time.fixedDeltaTime;

        /*ContactFilter2D filter = new ContactFilter2D ().NoFilter ();
        filter.SetLayerMask (LayerMask.GetMask ("Floor"));

        Collider2D[] array = new Collider2D[1];

        movementCollider.transform.localPosition = movement;

        movementCollider.OverlapCollider (filter, array);

        if (array[0]?.transform.position.z < transform.position.z) {*/

        RaycastHit2D t = Physics2D.BoxCast (transform.position, Vector2.one, 0, movement, movement.magnitude, LayerMask.GetMask ("Floor"));

        if (t.collider?.transform.position.z < transform.position.z) {

            //Debug.DrawLine (transform.position, t.point, Color.red, 100f);

            //Debug.DrawRay (t.point, -movement, Color.red, 100f);

            /*RaycastHit2D target = Physics2D.Raycast (transform.position, movement, movement.magnitude, LayerMask.GetMask ("Floor"));

            Debug.DrawRay (target.point, -movement, Color.red, 100f);

            movement = movement.normalized * t.distance;       */

            movement = Vector2.zero;

        }

        /*if (array[0]) {

            if (Input.GetButtonDown ("Jump") && !jumping) {



            }

        } else {

            height = Mathf.Max (deathHeight, height - gravity * Time.fixedDeltaTime);

        }

        transform.localScale = Vector3.one * Mathf.Lerp (1, minSpriteSize, Mathf.InverseLerp (0, deathHeight, height));*/

    }

}
