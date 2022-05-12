using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBar : MonoBehaviour
{
    public Slider slider;

    public void SetSnow(float snow)
    {
        slider.value = snow;
    }

    public void SetMaxSnow(float snow)
    {
        slider.maxValue = snow;
        slider.value = snow;
    }
}
