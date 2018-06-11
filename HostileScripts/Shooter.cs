using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public Enemy shooter;
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public GameObject ShotPrefab;
    private Rigidbody2D rb;
    public GameObject player;
    bool playerInRange;
    public float shotInterval;
    public List<Transform> Locations;
    GameObject NodesParent;
    Transform newTarget;
    int targetIndex;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        NodesParent = GameObject.Find("LocationNodes");
        for (int i =0; i > NodesParent.transform.childCount; i++)
        {
            Locations.Add(transform.GetChild(i));
        }
        target = GetTarget();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerInRange)
        {
            FireAtPlayer();
        }
        else
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
	}

    void FireAtPlayer()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        //Instantiate Shot, give direction
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "DeathWall")
        {
            shooter.ResetSelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            //stop moving
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            //stop moving
            playerInRange = false;
        }
    }

    private Transform GetTarget()
    {
        targetIndex = Random.Range(0, Locations.Count);
        newTarget = Locations[targetIndex];
        return newTarget;
    }
    public void ResetEnemy()
    {
        shooter.ResetSelf();
    }
}
