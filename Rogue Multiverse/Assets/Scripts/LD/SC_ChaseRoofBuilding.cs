using UnityEditor;
using UnityEngine;

public class SC_ChaseRoofBuilding : MonoBehaviour {

    public Vector2 Size;

    private void OnValidate () {

        EditorApplication.delayCall += () => {

            if (this) {

                GetComponent<SpriteRenderer> ().size = Size;

                transform.GetChild (0).transform.localScale = new Vector3 (Size.x, Size.y, 1);

            }

        };

    }

}
