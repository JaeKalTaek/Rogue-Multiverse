using UnityEngine;
using static SC_BasePlayerCharacter;

public class SC_Camera_RoofsChase : SC_Camera_Base {

    [Range(0.01f, 0.95f)]
    public float percentageScreenToMove;

    public float moveSpeed;    

    float PlayerHeight { get { return Cam.WorldToViewportPoint(Player.transform.position).y; } }

    float ScreenToMoveHeight { get { return Cam.ViewportToWorldPoint(Vector3.up * percentageScreenToMove).y; } }

    void Update() {

        if (PlayerHeight >= percentageScreenToMove)
            transform.Set(null, Mathf.Min (ScreenToMoveHeight, transform.position.y + moveSpeed * Time.deltaTime), null);        

    }

    /*public void Respawned (float baseCamDistance) {

        if (PlayerHeight < 0.1f)
            transform.Set(null, Player.transform.position.y + baseCamDistance, null);
        else if (PlayerHeight > .9f)
            transform.position += Vector3.up * Cam.ViewportToWorldPoint(Vector3.up * (PlayerHeight - percentageScreenToMove - .1f)).y * 2;

    }*/

}
