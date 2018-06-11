using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadOnClick : MonoBehaviour {

    public void LoadScene(int sceneIndex)
    {
        if(sceneIndex == 1)
        {
            GameManagment.instance.isGameOver = false;
            AudioManager.instance.inMenu = false;
            AudioManager.instance.UpdateAudioSorce();
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
