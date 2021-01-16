﻿using System;
using UnityEngine;
using static SC_StreetFight_Attack;

public class SC_PC_StreetFight : SC_BasePlayerCharacter {

    [Serializable]
    public struct AttackMapping {

        public string input;
        public string attack;

    }

    [Header ("Street Fight PC variables")]
    public AttackMapping[] attacksMapping;

    public BaseAttackVariables simplePunch, simpleKick;

    protected override Collider2D MovementCheck (Vector2 movement) {

        return Physics2D.BoxCast (new Vector3 (ColliderPos.x, transform.position.y + .1f), new Vector2 (collider.bounds.size.x, .2f), 0, movement, movement.magnitude, LayerMask.GetMask ("Default")).collider ??
            Physics2D.BoxCast (new Vector3 (ColliderPos.x, transform.position.y + .1f), collider.bounds.size / 2, 0, movement, movement.magnitude, LayerMask.GetMask ("Enemy")).collider;

    }

    protected override void AdditionalMovement () {

        Move (YMovement);

    }

    SC_StreetFight_Attack currentAttack;

    protected override void Update () {

        Vector3 prevPos = transform.position;

        base.Update ();

        animator.SetBool ("Walking", Vector3.Distance (prevPos, transform.position) > 0);

        if (!Paused) {

            foreach (AttackMapping am in attacksMapping)
                if (!currentAttack && Input.GetButtonDown (am.input))
                    currentAttack = StartAttack (gameObject, am.attack);

        }

    }

    public void StopAttack () {

        currentAttack.Stop ();

    }

    public void EndAttack () {

        Destroy (currentAttack);

    }

}