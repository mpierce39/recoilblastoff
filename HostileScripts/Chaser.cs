using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    //Variable Setup
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public Enemy chaser;
    public int scoreIncrease = 5;
    public List<AudioClip> enemyDeath;
    AudioSource myAudioSource;

    public Transform particleExplosion;

    //find Player
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        chaser.enemy = gameObject;
        chaser.myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        chaser.ChooseRandomClip();
    }

    //Move after player
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - chaser.myRigidBody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        chaser.myRigidBody.angularVelocity = -rotateAmount * rotateSpeed;
        chaser.myRigidBody.velocity = transform.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.collider.tag == "PlayerShot")
        {
            chaser.ReduceHealth(1);
        }
        if (collision.collider.tag == "Asteroid")
        {
            chaser.ResetSelf();
            
        }
        if (collision.collider.tag == "DeathWall")
        {
            chaser.ResetSelf();
        }
    }
    void Ondeath()
    {
        GameManagment.instance.IncreaseScore(scoreIncrease);

        var explosion = Instantiate(particleExplosion, transform.position, transform.rotation);
        AudioSource audio = explosion.gameObject.AddComponent<AudioSource>();
        audio.clip = enemyDeath[Random.Range(0, enemyDeath.Count - 1)];
        audio.PlayOneShot(audio.clip);

        Destroy(explosion.gameObject, 1);

        gameObject.SetActive(false);
    }
    public void ResetEnemy()
    {
        chaser.ResetSelf();
    }
}
