using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //Variable Setup
    public Sprite spriteToUse;
    public float ForceToAdd;
    SpriteRenderer spriteRend;
    GameObject parentGO;
    Rigidbody2D myRigidBody;
    PlayerController playerScript;
    //gets parent
    private void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        parentGO = GameObject.Find("Player");
        playerScript = parentGO.gameObject.GetComponent<PlayerController>();
    }
    //add force
    void OnEnable()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        parentGO = GameObject.Find("Player");
        playerScript = parentGO.gameObject.GetComponent<PlayerController>();
        myRigidBody.AddForce(playerScript.lookDir.up * ForceToAdd);
    }
    //Resets
    public void ResetShot()
    {
        myRigidBody.isKinematic = true;
        myRigidBody.isKinematic = false;
        gameObject.SetActive(false);        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "PowerUp") { 
            ResetShot();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "PowerUp"){
            ResetShot();
        }
    }


}
