using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StreetFight_Attack : MonoBehaviour {

    [Serializable]
    public struct BaseAttackVariables {

        public Collider2D collider;
        public int damage;
        public float speed;
        public float cooldown;

    }

    public enum AttackState { Ongoing, Ending, Cooldown }

    MonoBehaviour user;
    string attack;

    IEnumerator coroutine;

    public AttackState State { get; set; }

    BaseAttackVariables GetValues { get { return (BaseAttackVariables) user.GetType ().GetField (attack).GetValue (user); } }

    public static SC_StreetFight_Attack StartAttack (GameObject g, string a) {

        SC_StreetFight_Attack newAttack = g.AddComponent<SC_StreetFight_Attack> ();
        newAttack.StartAttack (a);
        return newAttack;

    }

    void StartAttack (string a) {

        user = gameObject.GetComponent<SC_BaseCharacter> ();
        attack = a;

        user.GetComponent<Animator> ().SetFloat (attack + "Speed", GetValues.speed);
        user.GetComponent<Animator> ().SetTrigger (attack);

        coroutine = Attack ();
        StartCoroutine (coroutine);

    }

    IEnumerator Attack () {

        List<Collider2D> hits = new List<Collider2D> ();

        while (State == AttackState.Ongoing) {

            List<Collider2D> results = new List<Collider2D> ();
            GetValues.collider.OverlapCollider (SC_ExtensionMethods.GetFilter ((user as SC_StreetFightEnemy_Base ? "Player" : "Enemy") + "Hitbox"), results);

            foreach (Collider2D c in results) {

                if (!hits.Contains (c)) {

                    c.GetComponentInParent<SC_BaseCharacter> ()?.Hit (GetValues.damage);

                    hits.Add (c);

                }

            }

            yield return new WaitForSeconds (Time.deltaTime);

        }

        while (State == AttackState.Ending) yield return null;

        yield return new WaitForSeconds (GetValues.cooldown);

        Destroy (this);

    }

}
