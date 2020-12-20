using UnityEngine;

public static class SC_ExtensionMethods {

    public static Vector2 V2 (this Vector3 v) {

        return new Vector2 (v.x, v.y);

    }

    public static Vector3 V3 (this Vector2 v) {

        return new Vector3 (v.x, v.y);

    }

    public static Vector3 Copy (this Vector3 v, float? x, float? y, float? z) {

        return new Vector3 (x ?? v.x, y ?? v.y, z ?? v.z);

    }

    public static float Length (this AnimationCurve a) {

        return a.keys[a.length - 1].time;

    }

}
