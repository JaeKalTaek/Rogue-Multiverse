using System.Collections.Generic;
using UnityEngine;
using static SC_BasePlayerCharacter;

public class SC_StreetFightEnemy_Base : SC_BaseCharacter {

    //[Header ("Base Street Fight enemy variables")]   

    protected static List<SC_StreetFightEnemy_Base> enemies;

    protected override void Start () {

        base.Start ();

        if (enemies == null)
            enemies = new List<SC_StreetFightEnemy_Base> ();

        enemies.Add (this);

    }

    public static void UpdateEnemies () {

        foreach (SC_StreetFightEnemy_Base e in enemies) {

            if (e) {

                Vector2 movement = (Player.transform.position - e.transform.position).normalized * e.moveSpeed * Time.deltaTime;

                if (!Player.Paused && !SC_GM_StreetFight.MovementCheck (e, movement)) {

                    e.transform.position += movement.V3 ();

                }

            }

        }

    }

    void OnDestroy () {

        enemies.Remove (this);

    }

}
