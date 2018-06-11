using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighScoreUI : MonoBehaviour {

    public Text highScoreText;

	// Use this for initialization
	void Start ()
    {

        if (GameManagment.instance.currentGameData.HighScore < 10)
        {
            highScoreText.text = "0" + GameManagment.instance.currentGameData.HighScore;
        }
        else
            highScoreText.text = GameManagment.instance.currentGameData.HighScore.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
