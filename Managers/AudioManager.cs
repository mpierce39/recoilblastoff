using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioClip mainMenu;
    public AudioClip inGame;
    public AudioClip inGame2;
    public bool inMenu;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        inMenu = true;
        UpdateAudioSorce();
    }

    public void UpdateAudioSorce()
    {
        musicSource.Stop();
        if (inMenu)
        {
            musicSource.clip = mainMenu;
            musicSource.volume = GameManagment.instance.musicLevel;
            musicSource.Play();
            Debug.Log("Menu MUSIC");
        }
        else
        {
            int clip = Random.Range(0, 1);
            if (clip == 1) musicSource.clip = inGame; else musicSource.clip = inGame2;
            musicSource.volume = GameManagment.instance.musicLevel;
            musicSource.Play();
            Debug.Log("Game MUSIC");
        }
        
    }

    public void ChangeVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    public void OnEnemyDeath(AudioClip clip,GameObject enemyGO,GameObject particleSystem)
    {
        GameObject tempGO = Instantiate(particleSystem, enemyGO.transform.position, enemyGO.transform.rotation);
        Destroy(tempGO, 1f);
        //Instanitate Here With particleSystem
        
        audioSource.PlayOneShot(clip);
        enemyGO.SetActive(false);
    }

    public void MusicStart()
    {
        musicSource.Play();
    }
}
