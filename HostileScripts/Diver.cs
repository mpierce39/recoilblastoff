using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : MonoBehaviour {
    //Enemy Var Setup
    public Enemy diver;
    public int health = 2;
    public float speed = 3f;
    public int score = 2;
    //This enemy only
    public GameObject target;
   
    //getting things
    private void OnEnable()
    {
        diver.myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        diver.enemy = gameObject;
        diver.health = health;
        diver.ChooseRandomClip();
        diver.myRigidBody.velocity = Vector2.zero;
        diver.myRigidBody.isKinematic = true;
        diver.myRigidBody.isKinematic = false;
        Debug.Log(diver.myRigidBody.velocity);
        FindPlayer();
        
    }
    //Find player start moving at there location
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(RandomWait());
        
        
    }
    //Fires itself in s straight line at the player
    private void Dive()
    {
        Vector2 direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        transform.up = direction;
        diver.myRigidBody.transform.up = direction;
        diver.myRigidBody.velocity = direction * speed;
    }
    private void ResetSelf()
    {
        diver.ResetSelf();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeathWall")
        {
            ResetSelf();
        }
        if (other.tag == "Hostile")
        {
            if (other.GetComponent<Chaser>() != null)
            {
                Chaser otherscript = other.GetComponent<Chaser>();
                otherscript.ResetEnemy();
            }
            if (other.GetComponent<Roamer>() != null)
            {
                Roamer otherscript = other.GetComponent<Roamer>();
                otherscript.ResetEnemy();
            }
            if (other.GetComponent<AsteroidSpawner>() != null)
            {
                AsteroidSpawner otherscript = other.GetComponent<AsteroidSpawner>();
                otherscript.ResetEnemy();
            }
            if (other.GetComponent<Shooter>() != null)
            {
                Shooter otherscript = other.GetComponent<Shooter>();
                otherscript.ResetEnemy();
            }
            if (other.GetComponent<Diver>() != null)
            {
                Diver otherscript = other.GetComponent<Diver>();
                otherscript.ResetEnemy();
            }
        }
        if(other.tag == "Player")
        { 
            if(other.GetComponent<PlayerController>() != null)
            {
                PlayerController otherscript = other.GetComponent<PlayerController>();
                otherscript.TakeDamage();
            }
        }
    }
    public IEnumerator RandomWait()
    {
        int timeToWaitBetweenMoves = Random.Range(1, 2);
        while (timeToWaitBetweenMoves > 0)
        {
            timeToWaitBetweenMoves--;
            yield return new WaitForSeconds(1f);
        }
        Dive();
    }
    public void ResetEnemy()
    {
        diver.ResetSelf();
    }
}
