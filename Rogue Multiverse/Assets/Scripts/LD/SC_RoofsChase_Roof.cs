﻿using System.Collections.Generic;
using UnityEngine;

public class SC_RoofsChase_Roof : MonoBehaviour {

    public Vector2 size;

    public float height;

    public Sprite buildingSprite;

    public void Setup() {

        transform.Set(null, null, -height);

        GetComponent<SpriteRenderer>().size = size;

        GetComponent<BoxCollider2D>().size = size;

        List<SpriteRenderer> f = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        f.Remove(GetComponent<SpriteRenderer>());

        foreach (SpriteRenderer sr in f)
            DestroyImmediate(sr.gameObject);

        for (int i = 0; i < height; i++) {

            SpriteRenderer sr = new GameObject().AddComponent<SpriteRenderer>();

            sr.transform.parent = transform;

            sr.drawMode = SpriteDrawMode.Sliced;

            sr.sprite = buildingSprite;

            float w = sr.sprite.bounds.size.x - 1;

            float h = sr.sprite.bounds.size.y - 1;

            sr.transform.localPosition = new Vector3(w * (i + .5f), h * -(i + .5f), 0);

            sr.transform.Set(null, null, transform.position.y);

            sr.size = new Vector2(size.x + w, size.y + h);

            /*Collider2D c = sr.gameObject.AddComponent<PolygonCollider2D>();

            Collider2D c2 = new GameObject().AddComponent<PolygonCollider2D>();

            c2 = c;

            c2.transform.parent = sr.transform;

            c2.isTrigger = true;

            c2.gameObject.layer = LayerMask.GetMask("Ignore Raycast");*/

        }

    }

}
