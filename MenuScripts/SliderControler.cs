using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControler : MonoBehaviour {

    public Slider slider;

    public void Add()
    {
        if(slider.value < 1f)
        {
            slider.value += .1f;
        }
        else
        {
            slider.value = 1f;
        }
    }

    public void Sub()
    {
        if (slider.value > 0f)
        {
            slider.value -= .1f;
        }
        else
        {
            slider.value = 0f;
        }
    }
}
