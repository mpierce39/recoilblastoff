using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    //creates pool
   public void CreateChildren(GameObject prefab)
    {
        PoolManager.instance.CreatePool(prefab, 10,gameObject.transform); 
    }
    //spawn shot prefab at given trasform
    public void ShootChild(GameObject prefab,Transform shooter)
    {
        Rigidbody2D rigidBody = prefab.GetComponent<Rigidbody2D>();
        PoolManager.instance.ReuseObject(prefab, shooter.position);
    }

}
