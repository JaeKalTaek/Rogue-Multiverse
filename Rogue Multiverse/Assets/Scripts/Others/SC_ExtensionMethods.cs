using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class SC_ExtensionMethods {

    public static Vector2 V2 (this Vector3 v) {

        return new Vector2 (v.x, v.y);

    }

    public static Vector3 V3 (this Vector2 v) {

        return new Vector3 (v.x, v.y);

    }

    public static void Set (this Transform t, float? x, float? y, float? z) {

        t.position = new Vector3 (x ?? t.position.x, y ?? t.position.y, z ?? t.position.z);

    }

    public static float Length (this AnimationCurve a) {

        return a.keys[a.length - 1].time;

    }

    public static void AddBefore (this TextMeshProUGUI t, string s) {

        t.text = s + t.text;

    }

    public static float B (this float f, bool b) {

        return f * (b ? 1 : -1);

    }

    public static float HalfWidth(this Camera c) {

        return c.aspect * c.orthographicSize;

    }

    public static float Width (this Camera c) {

        return c.HalfWidth() * 2f;

    }

    public static T RandomItem<T> (this List<T> list) {

        return list[Random.Range (0, list.Count)];

    }

    public static ContactFilter2D GetFilter (string layer) {

        ContactFilter2D c = new ContactFilter2D ();
        c.SetLayerMask (LayerMask.GetMask (layer));
        c.useTriggers = true;
        return c;

    }

}
