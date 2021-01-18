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

    public BaseAttackVariables simplePunch, simpleKick, comboPunch;

    [Header ("Punch combo variables")]
    public float comboTime;
    float comboTimer;

    public int comboHitsNeeded;
    int comboHits;

    protected override Collider2D MovementCheck (Vector2 movement) {
        
        return SC_GM_StreetFight.MovementCheck (this, movement);

    }

    protected override void AdditionalMovement () {

        Move (YMovement);

    }

    SC_StreetFight_Attack currentAttack;

    protected override void Update () {

        Vector3 prevPos = transform.position;

        base.Update ();

        animator.SetBool ("Walking", Vector3.Distance (prevPos, transform.position) > 0);        

        if (!Paused && !currentAttack) {

            comboTimer += comboHits == 0 ? 0 : Time.deltaTime;

            foreach (AttackMapping am in attacksMapping) {

                if (Input.GetButtonDown (am.input)) {                    

                    if (am.attack == "simplePunch") {                        

                        comboHits = comboTimer <= comboTime ? (comboHits >= comboHitsNeeded ? 0 : comboHits + 1) : 1;

                        currentAttack = StartAttack (gameObject, comboHits == 0 ? "comboPunch" : am.attack);

                    } else {

                        comboHits = 0;

                        currentAttack = StartAttack (gameObject, am.attack);

                    }

                    comboTimer = 0;

                }               

            }

        }        

    }

    public void StopAttack () {

        currentAttack.Stop ();

    }

    public void EndAttack () {

        Destroy (currentAttack);

    }

}