using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{
    //Prefab Scriptable Objects
    public Enemy asteroid;
    //Location The Asteroid Spawns
    public Transform spawnPoint;
    [HideInInspector]
    public Sprite spriteToUse;
    [HideInInspector]
    public SpriteRenderer spriteGettingUpdated;
    public Sprite[] healthValues;
    
    public  void Start()
    {
        spawnPoint = gameObject.transform;
        asteroid.myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        asteroid.enemy = gameObject;
        asteroid.health = 3;
        Debug.Log("Asteroid rigidbody is " + asteroid.myRigidBody);
        ChangeSprites(asteroid.health);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.collider.tag == "PlayerShot")
        {
            CustomOnHit(1);
            ChangeSprites(asteroid.health);
            GameObject shotGO = collision.gameObject;
            MoveGameObject(shotGO);
        }
        if (collision.collider.tag == "DeathWall")
        {
            asteroid.ResetSelf();
        }
    }
    void ResetGameObject()
    {
        gameObject.transform.position = spawnPoint.position;
    }
    //Moves Asteroid Based on Shot Location
    void MoveGameObject(GameObject shot)
    {
        asteroid.myRigidBody.AddRelativeForce(shot.transform.position);
    }
    void CustomOnHit(int healthLost)
    {
        asteroid.health -= healthLost;
        ChangeSprites(asteroid.health);
        if (asteroid.health < 1)
        {
            asteroid.OnDeath();
            asteroid.IncreaseScore();
        }
    }
    //Changes the asteroids sprite when it is damaged
    void ChangeSprites(int health)
    {
        switch (health)
        {
            case 3:
                {
                    spriteGettingUpdated = gameObject.GetComponent<SpriteRenderer>();
                    spriteGettingUpdated.sprite = spriteToUse = healthValues[0];
                    //Gets the asteroid sprite GameObjects
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        spriteGettingUpdated = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                        spriteGettingUpdated.sprite = healthValues[0];
                    }
                    break;
                }
            case 2:
                {
                    spriteGettingUpdated = gameObject.GetComponent<SpriteRenderer>();
                    spriteGettingUpdated.sprite = spriteToUse = healthValues[1];
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        spriteGettingUpdated = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                        spriteGettingUpdated.sprite = healthValues[1];
                    }
                    break;
                }
            case 1:
                {
                    spriteGettingUpdated = gameObject.GetComponent<SpriteRenderer>();
                    spriteGettingUpdated.sprite = spriteToUse = healthValues[2];
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        spriteGettingUpdated = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                        spriteGettingUpdated.sprite = healthValues[2];
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
