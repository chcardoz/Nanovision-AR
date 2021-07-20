using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutButton : MonoBehaviour
{

    [SerializeField] GameObject shrinkButton;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject spawnButton;

    void Start()
    {
        shrinkButton.SetActive(false);
        startButton.SetActive(false);
        spawnButton.SetActive(true);
    }

    void Update()
    {
        if (ShrinkPaper.objectIsPlaced)
        {
            startButton.SetActive(true);
            spawnButton.SetActive(false);
            if (Countdown.timerStarted)
            {
                shrinkButton.SetActive(true);
                startButton.SetActive(false);
            }
        }
    }
}
