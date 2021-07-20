using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountShrinks : MonoBehaviour
{
    [SerializeField] float maxNumShrink;
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = 0;
    }

    public void UpdateSliderVal()
    {
        slider.value = (float)Mathf.Clamp01((float)ShrinkPaper.numShrinks / maxNumShrink);
    }
}
