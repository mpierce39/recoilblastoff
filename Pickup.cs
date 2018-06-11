using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    GameObject prefab;

  
    public void Spawn()
    {
        Instantiate(prefab);
    }

}
