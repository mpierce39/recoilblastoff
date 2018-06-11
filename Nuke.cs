using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour {

    public GameObject spManager;
    public float flashlength = .25f;

    // Use this for initialization
    void OnEnable() {
        StartCoroutine(FlashTimer());
        //spManager.PoolResetActive();
    }

    private IEnumerator FlashTimer(){
        yield return new WaitForSeconds(flashlength);
        Destroy(this.gameObject);
    }
    
}
