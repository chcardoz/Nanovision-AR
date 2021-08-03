using UnityEngine;
using UnityEngine.UI;

public class CountShrinks : MonoBehaviour
{
    [SerializeField] float maxNumShrink;
    [SerializeField] Slider slider;

    /// <summary>
    /// Sets the slider value to zero initially
    /// </summary>
    void Start()
    {
        slider.value = 0;
    }


    /// <summary>
    /// Updates the value of the slide to match the number of times the user has shrunk the paper
    /// </summary>
    public void UpdateSliderVal()
    {
        slider.value = (float)Mathf.Clamp01((float)ShrinkPaper.numShrinks / maxNumShrink);
    }
}
