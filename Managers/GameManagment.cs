using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagment : MonoBehaviour {

    public static GameManagment instance;
    bool isAndroid = false;
    //Creates an instance that is perminant
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroid = true;
        }
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    //Globals
    public GameData currentGameData = new GameData();
    public GameData savedGames;
    public int Score = 0;
    public int highScore;
    public int Difficulty { get; set; }  
    public bool isGameOver = true;
    public float musicLevel = 1;
    public float sfxLevel = 1;
    public bool musicIsMuted = false;
    public bool allowShake = true;
    public bool firstTimePlaying = true;
    public bool watchingTutorial = false;
    public bool watchedAd = false;
    public GameObject pickup;

    public void Start()
    {     
        Load();
    }
    public void Save()
    {
        //References Binary Formatter which turns the code into machine language
        BinaryFormatter bf = new BinaryFormatter();
        //Creates a file saved under AppData named SavedGames.gd
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        //Game Data To Save
        GameData data = new GameData();
        
        //Makes the Data to Save equal the to current Game Data;
        data.HighScore = currentGameData.HighScore;
        data.firstTimePlaying = currentGameData.firstTimePlaying;
        //Action to serialize the data and store it in file
        bf.Serialize(file, data);
        //Closes the File
        file.Close();

    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            currentGameData.HighScore = data.HighScore;
            currentGameData.firstTimePlaying = data.firstTimePlaying;
        }
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        
    }
    private void Update()
    {
        if (isAndroid)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void SaveHighScore()
    {
        if (Score > highScore)
        {
            highScore = Score;
            if (highScore > currentGameData.HighScore)
            {
                currentGameData.HighScore = highScore;
                Save();
            }
            else
            {
                Save();
            }
        }
        Score = 0;
    }
   
    public void ChangeAudioLevel(float newLevel)
    {
        musicLevel = newLevel;
    }

    public void SetWatchingTutorial()
    {
        watchingTutorial = true;
        firstTimePlaying = false;
    }

    public void SpawnPickup(Vector3 spawnLocation)
    {
        Instantiate(pickup, spawnLocation, Quaternion.identity);
    }
}
