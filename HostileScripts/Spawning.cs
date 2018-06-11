using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {
    //Variable Setup
    public GameObject prefab;
    public int NumHostiles;
    private Vector3 spawnPoint;
    public float timer;
    SpawnManager Master;
    bool isSpawning = false;

    //Create Pool and Find manager
    private void Awake()
    {
        PoolManager.instance.CreatePool(prefab, NumHostiles, gameObject.transform);
        Master = GameObject.Find("SpawnManager").gameObject.GetComponent<SpawnManager>();       
    }
    //start the spawntimer
    private void OnEnable()
    {
        Invoke("CustomStart", 2f);
    }
    //Delay between spawning a new Hostile
    public IEnumerator SpawnTimer()
    {
        while (gameObject.activeSelf)
        {
            if (Master.allowSpawning) {
                spawnPoint = Master.ChooseSpawner();
                SpawnEnemy();
                
            }
            //Debug.Log("Time For Enemy to Spawn " + timer);
            yield return new WaitForSeconds(timer);
        }
       
    }
    //Spawn Hostile
    public void SpawnEnemy()
    {
        PoolManager.instance.ReuseObject(prefab, spawnPoint, Quaternion.identity);
    }

    //Sets spawn time delay
    public void SetSpawnTime(float timeValue)
    {
        timer = timeValue;
    }
    void CustomStart()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(SpawnTimer());
        }
    }
}