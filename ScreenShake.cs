using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShake : MonoBehaviour {

    private bool isShaking = false;
    public bool allowShake = true;
    public float shakeAmount;
    Toggle toggle;
    

    private void Start()
    {
              
    }
    void Update () {
		
        if (isShaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
	}

    public void ShakeScreen()
    {
        if (GameManagment.instance.allowShake)
        {
            Debug.Log("Shaking");
            Handheld.Vibrate();
            StartCoroutine("Shake");
        }
        
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        if (isShaking == false)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(0.3f);

        isShaking = false;
        transform.position = originalPos;
    }
    
}
