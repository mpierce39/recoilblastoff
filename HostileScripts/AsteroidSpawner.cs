using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    public Enemy asteroidSpawner;
    public GameObject asteroidPrefab;
    public GameObject asteroidParent;
    List<GameObject> waypoints = new List<GameObject>();
    public Transform placeToSpawnAsteroid;
    Animation asteroidSpawnAnim;
    public Transform waypointTargeted;

    int asteroidCount = 1;
    public int health;
    int lastWaypoint, randomWaypoint;  
    float timeToWaitBetweenMoves = 3f;
    public float timeToSpawnAsteroid = 2f;
    bool RandomStop = false;
    bool asteroidSpawning;

    private void OnEnable()
    {
        asteroidParent = GameObject.Find("AsteroidParent");
        //Creates New Asteroid
        asteroidCount = 1;
        asteroidSpawner.health = 2;
        asteroidSpawner.lifeSpriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        asteroidSpawner.shadowLifeSpriteRender = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        asteroidSpawner.ChangeHealthSprite();
        asteroidSpawner.myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        asteroidSpawner.enemy = gameObject;
        asteroidSpawner.ChooseRandomClip();
        //Finds Waypoints Parent Object
        GameObject waypointParent = GameObject.Find("Waypoints");
        //Gets All The Children in the waypointParent
        for (int i = 0; i < waypointParent.transform.childCount - 1; i++)
        {
            waypoints.Add(waypointParent.transform.GetChild(i).gameObject);
        }
        //Start Waiting
        StartCoroutine(RandomWait());      
    }
    //Spawns Asteroid Based on TimeToSpawn Asteroid 
    public IEnumerator SpawnAsteroid()
    {
        float timer = timeToSpawnAsteroid;
        while (timer > 0)
        {
            asteroidSpawning = true;
            timer--;
            yield return new WaitForSeconds(1f);
        }
        if (asteroidCount > 0)
        {
            //Play Animation Here 
            Instantiate(asteroidPrefab, placeToSpawnAsteroid.position, Quaternion.identity, asteroidParent.transform);
            asteroidCount--;
        }
        StartCoroutine(RandomWait());    
    }
    //Randomly waits at waypoint Target
    public IEnumerator RandomWait()
    {
        timeToWaitBetweenMoves = Random.Range(1, 3);
        while (timeToWaitBetweenMoves > 0)
        {
            timeToWaitBetweenMoves--;          
            yield return new WaitForSeconds(1f);
        }
        MoveToWayPoint();
        asteroidSpawning = false;
    }
    public void FixedUpdate()
    {
        if (!asteroidSpawning && timeToWaitBetweenMoves < 1)
        {
            Vector2 direction = (Vector2)waypoints[randomWaypoint].transform.position - asteroidSpawner.myRigidBody.position;
            //Normalize Direction Sets to 1,1
            direction.Normalize();
            //RotateAmount Object Must do To Reach destination
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            asteroidSpawner.myRigidBody.angularVelocity = -rotateAmount * 200f;
            asteroidSpawner.myRigidBody.velocity = transform.up * asteroidSpawner.speed;
            if (Vector2.Distance(asteroidSpawner.myRigidBody.position,waypoints[randomWaypoint].transform.position) <= .1f)
            {
                StartCoroutine(SpawnAsteroid());
                asteroidSpawner.myRigidBody.velocity = new Vector2(0, 0);
            }
        }
    }
    void MoveToWayPoint()
    {
        //Chooses RandomWayPoint On Map
        randomWaypoint = Random.Range(0, waypoints.Count - 1);
        //Stores Last Waypoint Ref
        lastWaypoint = randomWaypoint;
        waypointTargeted = waypoints[randomWaypoint].transform;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerShot")
        {
            asteroidSpawner.ReduceHealth(1);
        }
        if (collision.collider.tag == "DeathWall")
        {
            asteroidSpawner.ResetSelf();
        }
    }
    public void ResetEnemy()
    {
        asteroidSpawner.ResetSelf();
    }
}
