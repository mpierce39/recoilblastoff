using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy 
{
    public GameObject target;
    public Rigidbody2D myRigidBody;
    public List<AudioClip> enemyDeathAudios;    
    public GameObject enemyDeathParticles;
    [HideInInspector]
    public GameObject enemy;
    public AudioClip enemyDeathAudio;
    public SpriteRenderer lifeSpriteRenderer;
    public SpriteRenderer shadowLifeSpriteRender;
    
    public float speed;  
    public int amountScorePerKill,health;
    //Health Sprites
    public Sprite healthSpriteToUse;
    public Sprite[] healthSprites;
    //Shadow Sprites
    public Sprite healthSpriteShadow;
    public Sprite[] healthShadowSprites;

    public GameObject nuke;
  
    public void ReduceHealth(int healthLost)
    {
        health -= healthLost;
        ChangeHealthSprite();
        GettingDamaged(Camera.main.gameObject);

        if (health < 1)
        {
            OnDeath();
            IncreaseScore();
            //SpawnNuke();
        }
    }
    public void IncreaseScore()
    {
        GameManagment.instance.IncreaseScore(amountScorePerKill);
    }
    public void OnDeath()
    {
        AudioManager.instance.OnEnemyDeath(enemyDeathAudio, enemy, enemyDeathParticles);
    }
    public AudioClip ChooseRandomClip()
    {
        enemyDeathAudio = enemyDeathAudios[Random.Range(0, enemyDeathAudios.Count - 1)];
        return enemyDeathAudio;
    }
    public void ResetSelf()
    {
        health = 0;
        OnDeath();
    }
    void SpawnPickup()
    {
        int minChance = 1;
        int maxChance = 100;
        int randomChance = Random.Range(minChance, maxChance);
        
    }
    public void ChangeHealthSprite()
    {      
        switch (health)
        {
            case 2:
                healthSpriteToUse = healthSprites[0];
                lifeSpriteRenderer.sprite = healthSpriteToUse;
                healthSpriteShadow = healthShadowSprites[0];
                shadowLifeSpriteRender.sprite = healthSpriteShadow; 
                break;
            case 1:
                healthSpriteToUse = healthSprites[1];
                lifeSpriteRenderer.sprite = healthSpriteToUse;
                healthSpriteShadow = healthShadowSprites[1];
                shadowLifeSpriteRender.sprite = healthSpriteShadow;
                break;
            case 0:
                healthSpriteToUse = null;
                break;            
        }
    }
    public Sprite GetHealthSprite()
    {
        return healthSpriteToUse;        
    }
    public Sprite GetHealthShadowSprite()
    {
        return healthSpriteShadow;
    }

    private void GettingDamaged(GameObject damage)
    {
        damage.GetComponent<ScreenShake>().ShakeScreen();
    }

    private void SpawnNuke()
    {
        int RNG = Random.Range(0, 100);
        if (RNG >= 95)
        {
            GameManagment.instance.SpawnPickup(enemy.transform.position);
        }
    }
}
