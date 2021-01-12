using System.Collections.Generic;
using UnityEngine;
using static SC_Camera_Base;
using static SC_BasePlayerCharacter;

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

    }

}
