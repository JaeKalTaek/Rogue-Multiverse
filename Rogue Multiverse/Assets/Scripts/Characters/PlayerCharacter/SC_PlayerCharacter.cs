using System.Collections.Generic;
using UnityEngine;
using static SC_Traits;

public class SC_PlayerCharacter : SC_BaseCharacter {

    public Alignment PlayerAlignment { get; set; }
    public OriginStory PlayerOriginStory { get; set; }
    public List<Speciality> PlayerSpecialities { get; set; }

    [Header ("Tweakable")]
    public float accelerationTime;
    public float moveSpeed;
    public float gravity;
    public float airControl;
    public float jumpHeight, jumpDuration;

    public LayerMask floorLayerMask;

    float currentAcceleration;
    float jumpStart, jumpTime;

    void Start () {

        /*PlayerAlignment = GetRandomTrait<Alignment> ();
        PlayerOriginStory = GetRandomTrait<OriginStory> ();
        PlayerSpecialities = new List<Speciality> () { GetRandomTrait<Speciality> () };*/

        jumpTime = -1;

    }

    void FixedUpdate () {

        Vector2 movement = new Vector2 ();

        bool grounded = Physics2D.Raycast (transform.position, Vector2.down, .57f, floorLayerMask);

        if (jumpTime < 0) {

            if (!grounded)
                movement += Vector2.down * gravity * Time.fixedDeltaTime;
            else if (Input.GetAxisRaw ("Vertical") > 0) {

                jumpTime = 0;

                jumpStart = transform.position.y;

            }

        }

        if (jumpTime >= 0) {

            jumpTime += Time.fixedDeltaTime;

            float lerp = jumpTime / jumpDuration;

            movement += Vector2.up * (Mathf.Lerp (jumpStart, jumpStart + jumpHeight, lerp) - transform.position.y);
            
            jumpTime = (lerp >= 1) || (Input.GetAxis ("Vertical") <= 0) ? -1 : jumpTime;

        }

        currentAcceleration = Mathf.Clamp (currentAcceleration += Time.fixedDeltaTime * (Input.GetAxis ("Horizontal") != 0 ? 1 : -1), 0, accelerationTime);

        movement += Vector2.right * Input.GetAxis ("Horizontal") * Mathf.Lerp (0, moveSpeed, currentAcceleration / accelerationTime) * (grounded || (jumpTime >= 0) ? 1 : airControl) * Time.fixedDeltaTime;

        RaycastHit2D target = Physics2D.Raycast (transform.position, movement, movement.magnitude + .5f);

        //transform.position += new Vector3 (movement.x, movement.y);

        GetComponent<Rigidbody2D> ().MovePosition (transform.position.V2 () + movement);

        if (/*!*/target.collider) {
            //transform.position += new Vector3 (movement.x, movement.y);
        //else {

            //transform.position = target.point - (movement.normalized * .5f);

            jumpTime = -1;

        }

    }      

}

