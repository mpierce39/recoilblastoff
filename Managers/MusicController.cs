using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

    bool isChanging = false;
    bool wantChange = false;

    public void SetMusicVolume(float volume)
    {
        GameManagment.instance.ChangeAudioLevel(volume);
        AudioManager.instance.UpdateAudioSorce();
    }

    public void SetSFXVolume(float volume)
    {
        GameManagment.instance.sfxLevel = volume;
        AudioManager.instance.ChangeVolume(volume);
    }
}
