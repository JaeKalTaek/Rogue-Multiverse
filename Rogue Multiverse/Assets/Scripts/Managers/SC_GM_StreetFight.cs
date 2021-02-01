using System.Collections.Generic;
using UnityEngine;
using static SC_Camera_Base;
using static SC_BasePlayerCharacter;
using UnityEngine.UI;

public class SC_GM_StreetFight : SC_GameManager {

    [Header ("Street Fight variables")]
    public float levelLength;
    public Vector2 buildingsSpacing;    

    [Header ("Sprites")]
    public List<SpriteRenderer> tiledSprites;
    public List<Sprite> buildings;

    [Header ("Enemies")]
    public Transform firstEnemiesSpawnPoints;
    public List<GameObject> enemyPrefabs;

    [Header ("UI")]
    public Image healthBar;

    List<Transform> characters;

    protected override void Start () {

        base.Start ();

        foreach (SpriteRenderer s in tiledSprites)
            s.size = new Vector2 (levelLength * Cam.Width(), s.size.y);

        SpriteRenderer lastBuilding = null;

        do {

            SpriteRenderer newBuilding = new GameObject ().AddComponent<SpriteRenderer> ();
            newBuilding.sprite = buildings.RandomItem ();
            newBuilding.sortingOrder = 1;
            newBuilding.transform.parent = tiledSprites[2].transform;
            newBuilding.transform.localPosition = Vector3.right * ((lastBuilding?.transform.localPosition.x ?? 0) + (lastBuilding?.sprite.bounds.size.x ?? 0) + Random.Range(buildingsSpacing.x, buildingsSpacing.y));
            lastBuilding = newBuilding;

        } while (lastBuilding.transform.localPosition.x < levelLength * Cam.Width ());

        characters = new List<Transform> () { Player.transform };

        foreach (Transform t in firstEnemiesSpawnPoints)
            characters.Add (Instantiate (enemyPrefabs[0], t.position, Quaternion.identity).transform);

    }

    void Update () {

        for (int i = 0; i < characters.Count; i++) {

            if (!characters[i])
                characters.RemoveAt (i);
            else
                characters[i].Set (null, null, characters[i].position.y);

        }

        SC_StreetFightEnemy_Base.UpdateEnemies ();

    }

    public static Collider2D MovementCheck (SC_BaseCharacter c, Vector2 movement) {

        RaycastHit2D[] charas = Physics2D.BoxCastAll (c.ColliderPos.V2() + movement, c.Collider.bounds.size, 0, movement, movement.magnitude, LayerMask.GetMask (new string[] { "Enemy", "Player" }));

        Collider2D charaHit = null;

        foreach (RaycastHit2D ch in charas)
            charaHit = ch.collider.GetComponentInParent<SC_BaseCharacter> () == c ? charaHit : ch.collider;

        return Physics2D.BoxCast (new Vector3 (c.ColliderPos.x, c.transform.position.y + .1f), new Vector2 (c.Collider.bounds.size.x * 2, .2f), 0, movement, movement.magnitude, LayerMask.GetMask ("Default")).collider ?? charaHit;

    }

    public void SetHealth () {

        healthBar.transform.localScale = new Vector3 ((float)Player.Health / Player.baseHealth, 1, 1);
        healthBar.color = Color.Lerp (Color.red, Color.green, (float) Player.Health / Player.baseHealth);

    }

}
