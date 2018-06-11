using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    #region Global Variables
    //Mouse Gamobject that follows mouse
    public GameObject mouseGO;
    Transform mousePosition;
    [Header("Turn Speed The player Moves towards the mouse and The Force Added To the Player During a Mouse Click")]
    public float turnSpeed = 50;


    //Rigid Body Attached to the playerObject
    Rigidbody2D myRigidBody;
    //Used For Debug (Draws Line)
    public Transform lookDir;
    [Header("Time to Respawn the Player After Death")]
    public float timeToRespawn, forceToAdd;
    private float timeToRespawnMax;
    //Sprite Renderer Attatched To the Player
    SpriteRenderer sprite;
    //Sprite of Player
    public Sprite spriteToUse;
    bool isRespawning;
    ShootScript shooterScript;
    public GameObject shadowGO;
    public GameObject playerLives;

    public int health/* = 3*/;
    public Sprite[] playerHealthSprite, playerHealthSpriteShadow;
    //public SpriteRenderer[] shadowSprite;

    SpriteRenderer playerLivesSprites;
    public AudioClip[] shootAudio;
    public AudioClip deathAudio;

    public GameObject shotPrefab, shotSpawnLocation;
    public GameObject deathParticles;

    public SpawnManager spScript;
    public UIManager uiScript;

    public GameObject nukeFlash;

    #endregion

    private void Start()
    {
        //Gets The Rigid Body Attatched to this Game Object
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        //Gets the Sprite Renderer attatched to this game object
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //Gets The Shot Manager Script attatched to this Game Object
        shooterScript = GameObject.Find("ShotManager").GetComponent<ShootScript>();
        //Assigns the Sprites to the player lives array
        playerLivesSprites = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        //Creates the pool of ShotPrefabs
        shooterScript.CreateChildren(shotPrefab);
        //Resets the Time to Respawn in the editor
        timeToRespawnMax = timeToRespawn;
        //Finds the Spawn Script
        spScript = GameObject.Find("SpawnManager").gameObject.GetComponent<SpawnManager>();
        //Finds the UI Script
        uiScript = GameObject.Find("CanvasUI").gameObject.GetComponent<UIManager>();
        health = 3;
        PlayerHealthSpriteChanger(health);
        GetComponent<AudioSource>().volume = GameManagment.instance.sfxLevel;
    }


    private void Update()
    {


        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && !isRespawning)
            {
                //Gets the Mouse Position
                Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseGO.transform.position = mouseScreenPosition;
                // get direction you want to point at
                Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
                // set vector of transform directly
                transform.up = direction;
                shooterScript.ShootChild(shotPrefab, gameObject.transform);
                //Instantiate(shotPrefab,shotSpawnLocation.transform.position,Quaternion.identity);
                myRigidBody.AddForce(-lookDir.up * forceToAdd);
                GetComponent<AudioSource>().PlayOneShot(shootAudio[Random.Range(0, shootAudio.Length)]);
            }
        }

        PlayerHealthSpriteChanger(health);
    }
    // Collider hits another collider
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "DeathWall" || collision.collider.tag != "PowerUp")
        {
            TakeDamage();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUp")
        {
            //Why you no work sometimes
            GameObject prefab = Instantiate(nukeFlash, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(collision.gameObject);
        }
    }
    //Resets Game Character when death occured
    public IEnumerator ResetGameCharacter()
    {
        spScript.SpawningSwitch();
        //References music audio source on player
        AudioManager.instance.musicSource.volume = GameManagment.instance.musicLevel * .15f;
        GetComponent<AudioSource>().volume = 1;
        GetComponent<AudioSource>().PlayOneShot(deathAudio);

        timeToRespawnMax = deathAudio.length + 0.3f;
        spScript.PoolResetActive();
        while (timeToRespawnMax > 0)
        {
            timeToRespawnMax--;
            isRespawning = true;
            //Makes the Character Disappear during while loop
            sprite.sprite = null;
            shadowGO.SetActive(false);
            playerLives.SetActive(false);
            myRigidBody.velocity = new Vector2(0, 0);
            gameObject.transform.position = new Vector3(0, 0, 1);
            GettingDamaged(Camera.main.gameObject);
            yield return new WaitForSeconds(1f);
        }
        GetComponent<AudioSource>().volume = 0.4f;
        AudioManager.instance.musicSource.volume = GameManagment.instance.musicLevel;
        //Resets the Player to Spawn Position
        sprite.sprite = spriteToUse;
        shadowGO.SetActive(true);
        playerLives.SetActive(true);
        //gameObject.transform.position = new Vector3(0, 0, 1);
        //timeToRespawnMax = timeToRespawn;
        spScript.SpawningSwitch();
        isRespawning = false;

        //Resets the Sprite

    }

    void PlayerHealthSpriteChanger(int health)
    {
        //HealthCount(playerHealthSprite, false);
        //HealthCount(playerHealthSpriteShadow, false);

        if (health != 0)
        {
            playerLivesSprites.sprite = playerHealthSprite[health - 1];
        }

        //shadowSprite.sprite = playerHealthSpriteShadow[health];
    }
    public void UseNuke()
    {
        List<GameObject> enemies = new List<GameObject>();
        List<GameObject> spawners = new List<GameObject>();
        GameObject spawnersParent = GameObject.Find("Spawners");

        for (int i = 0; i < spawnersParent.transform.childCount - 1; i++)
        {
            GameObject spawner = spawnersParent.transform.GetChild(i).gameObject;
            spawners.Add(spawner);
            for (int x = 0; x < spawners[i].transform.childCount - 1; x++)
            {
                GameObject enemy = spawners[i].transform.GetChild(x).gameObject;
                enemies.Add(enemy);
            }
        }
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.OnDeath();
        }
    }
    //This is so the Diver can kill the player
    public void TakeDamage()
    {
        health--;

        GameObject tempGO = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(tempGO, 1f);

        if (health == 0 && !GameManagment.instance.watchedAd)
        {
            uiScript.ShowAdPanel();
            uiScript.ShowGameOverPanel();
            SpawnManager spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawner.PoolResetActive();
        }
        else if (health == 0 && GameManagment.instance.watchedAd)
        {
            uiScript.ShowGameOverPanel();
            GameManagment.instance.isGameOver = true;
        }

        StartCoroutine(ResetGameCharacter());
    }

    private void GettingDamaged(GameObject damage)
    {
        damage.GetComponent<ScreenShake>().ShakeScreen();
    }
}
