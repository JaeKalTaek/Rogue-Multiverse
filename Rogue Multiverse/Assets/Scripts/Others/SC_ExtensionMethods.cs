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

}
