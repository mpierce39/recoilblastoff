using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleableBoarders : MonoBehaviour {
    //General
    private Vector2 screenSize;
    private Vector2 cameraPos;
    public float spaceing = 5;
    //Normal walls
    public GameObject TopBoarder;
    public GameObject BottomBoarder;
    public GameObject LeftBoarder;
    public GameObject RightBoarder;
    //Extra Deadly walls
    public GameObject TopBoarder_1;
    public GameObject BottomBoarder_1;
    public GameObject LeftBoarder_1;
    public GameObject RightBoarder_1;

    // Use this for initialization
    void Start () {
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        //Normal
        RightBoarder.transform.position = new Vector2(cameraPos.x + screenSize.x, cameraPos.y);
        LeftBoarder.transform.position = new Vector2(cameraPos.x - screenSize.x, cameraPos.y);
        TopBoarder.transform.position = new Vector2(cameraPos.x, cameraPos.y + screenSize.y);
        BottomBoarder.transform.position = new Vector2(cameraPos.x, cameraPos.y - screenSize.y);

        RightBoarder_1.transform.position = new Vector2((cameraPos.x + screenSize.x) + spaceing, cameraPos.y);
        LeftBoarder_1.transform.position = new Vector2((cameraPos.x - screenSize.x) - spaceing, cameraPos.y);
        TopBoarder_1.transform.position = new Vector2(cameraPos.x, (cameraPos.y + screenSize.y) + spaceing);
        BottomBoarder_1.transform.position = new Vector2(cameraPos.x, (cameraPos.y - screenSize.y) - spaceing);
    }
}
