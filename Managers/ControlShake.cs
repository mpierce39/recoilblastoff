using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShake : MonoBehaviour {

    
    public void SetAllowShake(bool allowShakes)
    {
        GameManagment.instance.allowShake = allowShakes;
    }
}
