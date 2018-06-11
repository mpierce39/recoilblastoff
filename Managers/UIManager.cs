using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject AdPanel;

    //Find GameOverPanel
    void Start()
    {
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        HideGameOverPanel();
    }

    //Display Score
    void Update()
    {
        if (GameManagment.instance.Score < 10)
        {
            scoreText.text = "0" + GameManagment.instance.Score;
        }
        else
            scoreText.text = GameManagment.instance.Score.ToString();
    }

    //Hide or show the GameOverPanel
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    public void ShowAdPanel()
    {
        AdPanel.SetActive(true);

    }
    public void HideAdPanel()
    {
        AdPanel.SetActive(false);
    }
    public void ResetAdWatch()
    {
        GameManagment.instance.watchedAd = false;
    }


}

