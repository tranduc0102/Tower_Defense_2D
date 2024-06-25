using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHP(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHP(float value)
    {
        slider.value = value;
    }
}
