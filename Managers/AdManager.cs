using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    public GameObject player;
    bool watchedAdOnce = false;
    private void Start()
    {
        player = GameObject.Find("Player");       
        Advertisement.Initialize("1748116");
    }

    public void WatchAdForLive()
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = LiveReward;

        Advertisement.Show("rewardedVideo", so);
    }

    private void LiveReward(ShowResult obj)
    {
        if(obj == ShowResult.Finished && !GameManagment.instance.watchedAd)
        {
            Debug.Log("watched Ad");
            PlayerController playerScript = player.GetComponent<PlayerController>();
            playerScript.health = 3;
            GameManagment.instance.watchedAd = true;
        }
    }
        
}
