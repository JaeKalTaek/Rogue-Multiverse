using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;
using static SC_StreetFight_Attack;

public class SC_StreetFightEnemy_Base : SC_BaseCharacter {

    [Header ("Base Street Fight enemy variables")]
    public BaseAttackVariables simplePunch;

    SC_StreetFight_Attack currentAttack;

    protected static List<SC_StreetFightEnemy_Base> enemies;

    protected override void Start () {

        base.Start ();

        if (enemies == null)
            enemies = new List<SC_StreetFightEnemy_Base> ();

        enemies.Add (this);

    }

    public static void UpdateEnemies () {

        foreach (SC_StreetFightEnemy_Base e in enemies) {

            if (e && !Player.Paused && !e.currentAttack) {

                Vector2 movement = (Player.transform.position - e.transform.position).normalized * e.moveSpeed * Time.deltaTime;

                Collider2D c = SC_GM_StreetFight.MovementCheck (e, movement);

                e.animator.SetBool ("Walking", !c);

                if (!c) {

                    e.transform.position += movement.V3 ();

                    if (movement.x != 0)
                        e.spriteR.flipX = movement.x < 0;

                } else if (c.GetComponentInParent<SC_BasePlayerCharacter> ()) {

                    e.currentAttack = StartAttack (e.gameObject, "simplePunch");

                }

            }

        }

    }

    public void StopAttack () {

        currentAttack.State = AttackState.Ending;

    }

    public void EndAttack () {

        currentAttack.State = AttackState.Cooldown;

    }

    void OnDestroy () {

        enemies.Remove (this);

    }

}
