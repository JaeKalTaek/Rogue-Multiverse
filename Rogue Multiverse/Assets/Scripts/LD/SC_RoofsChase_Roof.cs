using UnityEditor;
using UnityEngine;

public class SC_RoofsChase_Roof : MonoBehaviour {

    public Vector2 Size;

    public float height;

    private void OnValidate () {

        EditorApplication.delayCall += () => {

            if (this) {

                transform.position = transform.position.Copy (null, null, -height);

                GetComponent<SpriteRenderer> ().size = Size;

                GetComponent<BoxCollider2D> ().size = Size;                

                Transform rightShadow = transform.GetChild (1);

                rightShadow.localPosition = Size / 2;

                rightShadow.localScale = rightShadow.localScale.Copy (null, Size.y, null);

                Transform bottomShadow = transform.GetChild (0);

                bottomShadow.localPosition = Size / -2;

                bottomShadow.localScale = new Vector3 (Size.x + rightShadow.localScale.x * .1f, height * 5, 1);

            }

        };

    }

}
