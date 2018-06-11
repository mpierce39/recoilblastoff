using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    //loads sceen at given Index
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex == 1)
        {
            GameManagment.instance.isGameOver = false;
        }
        if(sceneIndex == 0)
        {
            AudioManager.instance.inMenu = true;
            AudioManager.instance.UpdateAudioSorce();
        }
        GameManagment.instance.SaveHighScore();
        SceneManager.LoadScene(sceneIndex); 
    }

}
