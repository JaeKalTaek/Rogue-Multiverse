using UnityEngine;

public static class SC_ExtensionMethods {

    public static Vector2 V2 (this Vector3 v) {

        return new Vector2 (v.x, v.y);

    }

    public static Vector3 V3 (this Vector2 v) {

        return new Vector3 (v.x, v.y);

    }

}
