﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StreetFight_Attack : MonoBehaviour {

    [Serializable]
    public struct BaseAttackVariables {

        public Collider2D collider;
        public int damage;
        public float speed;

    }

    MonoBehaviour user;
    string attack;

    IEnumerator coroutine;

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

        while (true) {

            List<Collider2D> results = new List<Collider2D> ();
            GetValues.collider.OverlapCollider (SC_ExtensionMethods.GetFilter ("EnemyHitbox"), results);

            foreach (Collider2D c in results) {

                if (!hits.Contains (c)) {

                    c.GetComponentInParent<SC_BaseCharacter> ()?.Hit (GetValues.damage);

                    hits.Add (c);

                }

            }

            yield return new WaitForSeconds (Time.deltaTime);

        }

    }

    public void Stop () {

        StopCoroutine (coroutine);

    }

}