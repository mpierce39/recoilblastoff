using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<GameObject> spawners = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();
    Spawning spawnScript;
    GameManagment gmScript;
    public int SpawnCount = 0;
    public float deathReactivationDelay = 1.5f;
    public bool allowSpawning = true;

    private void Start()
    {
        gmScript = GameManagment.instance;
        //Fills spawnPoints List
        for (int x = 0; x < transform.childCount; x++)
        {
            spawnPoints.Add(transform.GetChild(x));
            SpawnCount++;
        }
    }

    public void Update()
    {
        UpdateSpawners();
        //Stops Spawning things when game is Over
        if (gmScript.isGameOver)
        {
            for(int i = 0; i<spawners.Count;i++)
            {
                spawners[i].SetActive(false);
            }
        }
    }

    public void UpdateSpawners()
    {
        //looks at the score
        int score = gmScript.Score;
        
        //Turns things on accordingly 
        switch (score)
        {
            case 0:
                {
                    ReactiveSpawns(4);
                    AdjustSpawnRate(4, 5f);
                    break;
                }
            case 5:
                {
                    ReactiveSpawns(0);
                    DeactiveSpawns(4);
                    AdjustSpawnRate(0, 3f);
                    break;
                }
            case 10:
                {
                    ReactiveSpawns(1);
                    AdjustSpawnRate(1, 8f);
                    break;
                }
            case 15:
                {
                    AdjustSpawnRate(1, 7f);
                    break;
                }
            case 20:
                {
                    ReactiveSpawns(2);
                    AdjustSpawnRate(2, 5f);
                    AdjustSpawnRate(1, 3.5f);
                    AdjustSpawnRate(0, 2f);
                    break;
                }
            case 30:
                {
                    ReactiveSpawns(3);
                    AdjustSpawnRate(3, 5f);
                    AdjustSpawnRate(2, 2.5f);
                    break;
                }
            case 40:
                {
                    AdjustSpawnRate(0, 1f);
                    break;
                }
            case 45:
                {
                    AdjustSpawnRate(1, 3f);
                    break;
                }
            case 50:
                {
                    AdjustSpawnRate(3, 2f);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    //returns a spawn location
    public Vector3 ChooseSpawner()
    {
        Vector3 SpawnPoint;
        int chooseSpawn = Random.Range(0, SpawnCount - 1);
        SpawnPoint = spawnPoints[chooseSpawn].transform.position;

        return SpawnPoint;
    }
    //Disables Spawners during player respawn time and kills all hostiles on screen
    public void PoolResetActive()
    {
        for (int spawnerIndex = 0; spawnerIndex < spawners.Count; spawnerIndex++)
        {
            for (int childIndex = 0; childIndex < spawners[spawnerIndex].transform.childCount; childIndex++)
            {
                GameObject childobject = spawners[spawnerIndex].transform.GetChild(childIndex).gameObject;
                childobject.SetActive(false);
            }
        }
        GameObject Asteroidparent = GameObject.Find("AsteroidParent");
        for (int asteroidIndex = 0; asteroidIndex < Asteroidparent.transform.childCount; asteroidIndex++)
        {
            GameObject curAsteroid = Asteroidparent.transform.GetChild(asteroidIndex).gameObject;
            curAsteroid.SetActive(false);
        }
    }
    //reenables apropriate spawner
    public void ReactiveSpawns(int spawner)
    {
        spawners[spawner].SetActive(true);
    }
    //Disables appropriate spawner
    public void DeactiveSpawns(int spawner)
    {
        spawners[spawner].SetActive(false);
    }
    //Gives spawner new spawn delay
    public void AdjustSpawnRate(int spawner, float newDelay)
    {
        Spawning spawnerScript;
        spawnerScript = spawners[spawner].gameObject.GetComponent<Spawning>();
        spawnerScript.timer = newDelay;
    }
    public void SpawningSwitch()
    {
        allowSpawning = !allowSpawning;
    }
}
