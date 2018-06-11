using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtnLoadOnClick : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Animator menuAnim;
    
    public void LoadNewScene(string anim)
    {
        menuAnim = MenuCanvas.GetComponent<Animator>();
        menuAnim.Play("CanvasOnPlay");
        StartCoroutine(LoadInBackground());
    }

    IEnumerator LoadInBackground()
    {
        if (GameManagment.instance.firstTimePlaying)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(4);
            GameManagment.instance.firstTimePlaying = false;

            loading.allowSceneActivation = false;

            while (!loading.isDone)
            {
                if (loading.progress == 0.9f)
                {
                    loading.allowSceneActivation = true;
                }
                yield return null;
            }
        }
        else
        {
            GameManagment.instance.isGameOver = false;
            AsyncOperation loading = SceneManager.LoadSceneAsync(1);
            AudioManager.instance.inMenu = false;
            AudioManager.instance.UpdateAudioSorce();

            AudioManager.instance.musicSource.Stop();
            loading.allowSceneActivation = false;

            while (!loading.isDone)
            {
                if (loading.progress == 0.9f)
                {
                    loading.allowSceneActivation = true;
                }
                yield return null;
            }
        }
        
        
        
    }
}
