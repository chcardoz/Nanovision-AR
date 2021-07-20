using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowControls : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject spawnButton;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject toggleButtonObject;
    [SerializeField] GameObject arCamera;
    private bool isActive = false;
    private Text toggleButtonText;

    private void Start()
    {
        startButton.SetActive(false);
        controls.SetActive(false);
        spawnButton.SetActive(true);
        arCamera.GetComponent<Magnification>().enabled = isActive;
        toggleButtonText = toggleButtonObject.GetComponent<Text>();
    }
    public void Show()
    {
        startButton.SetActive(false);
        controls.SetActive(true);
    }

    public void ShowStartButton()
    {
        startButton.SetActive(true);
        spawnButton.SetActive(false);
    }

    public void ToggleLens()
    {
        isActive = !isActive;
        arCamera.GetComponent<Magnification>().enabled = isActive;
        if (isActive)
        {
            toggleButtonText.text = "hide";
        }
        else
        {
            toggleButtonText.text = "show";
        }
    }
}
