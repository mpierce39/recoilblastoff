using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateThings : MonoBehaviour {

    public bool NotFinished = true;

    public void OnEnable()
    {
        CustomStart();
    }

    void CustomStart()
    {
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.UpdateAudioSorce();
            NotFinished = false;
        }
    }
}
