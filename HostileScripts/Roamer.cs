using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Roamer : MonoBehaviour
{
    //Enemy Var Setup
    public Enemy roamer;
    public int health = 2;
    //My variables
    List<GameObject> waypoints = new List<GameObject>();
    public Transform waypointTargeted;
    int lastWaypoint, randomWaypoint;
    float timeToWaitBetweenMoves = 0f;
    bool RandomStop = false;

    private void OnEnable()
    {
        //Creates New Roamer
        roamer.health = health;       
        roamer.lifeSpriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        roamer.shadowLifeSpriteRender = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        roamer.ChangeHealthSprite();
        roamer.myRigidBody = GetComponent<Rigidbody2D>();
        roamer.enemy = gameObject;
        roamer.enemyDeathAudio = roamer.ChooseRandomClip();
        //Finds Waypoints Parent Object
        GameObject waypointParent = GameObject.Find("Waypoints");
        //Gets All The Children in the waypointParent
        for (int i = 0; i < waypointParent.transform.childCount - 1; i++)
        {
            waypoints.Add(waypointParent.transform.GetChild(i).gameObject);
        }
        //Start Waiting
        MoveToWayPoint();
    }
   
    public void FixedUpdate()
    {
        Vector2 direction = (Vector2)waypoints[randomWaypoint].transform.position - roamer.myRigidBody.position;
        //Normalize Direction Sets to 1,1
        direction.Normalize();
        //RotateAmount Object Must do To Reach destination
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        roamer.myRigidBody.angularVelocity = -rotateAmount * 200f;
        roamer.myRigidBody.velocity = transform.up * roamer.speed;
        if (Vector2.Distance(roamer.myRigidBody.position, waypoints[randomWaypoint].transform.position) <= .4f)
        {
            MoveToWayPoint();
            roamer.myRigidBody.velocity = new Vector2(0, 0);
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerShot")
        {
            roamer.ReduceHealth(1);
        }
        if (other.collider.tag == "Asteroid")
        {
            roamer.ReduceHealth(1);
        }
        if (other.collider.tag == "DeathWall")
        {
            roamer.ResetSelf();
        }
    }
    public void ResetEnemy()
    {
        roamer.ResetSelf();
    }




}
