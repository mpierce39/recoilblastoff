using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    static PoolManager _instance;
    //prevents multiple instances from being created
    public static PoolManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }
    //Creates a pool of given size of given prefab
    public void CreatePool(GameObject prefab, int poolSize){
        int poolKey = prefab.GetInstanceID();
        if(!poolDictionary.ContainsKey(poolKey)){
            poolDictionary.Add(poolKey, new Queue<GameObject>());
            
            for(int i = 0; i <poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }
    //Creates a pool of given size of given prefab under given parent
    public void CreatePool(GameObject prefab, int poolSize,Transform parent)
    {
        int poolKey = prefab.GetInstanceID();
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab,parent) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }
    //turns on the next instance of given prefab at given location and given rotaion
    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {

            GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            if (!objectToReuse.activeSelf) { 
                objectToReuse.SetActive(true);
                objectToReuse.transform.position = position;
                objectToReuse.transform.rotation = rotation;
                
            }
        }
    }
    //turns on the next instance of given prefab at given location
    public void ReuseObject(GameObject prefab, Vector3 position)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictionary[poolKey].Dequeue();           
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.SetActive(true);          
            objectToReuse.transform.position = position;
          
        }
    }
    
}
