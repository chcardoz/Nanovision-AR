using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutButton : MonoBehaviour
{

    [SerializeField] GameObject cutButton;
    [SerializeField] GameObject startButton;

    void Start()
    {
        cutButton.SetActive(false);
        startButton.SetActive(true);
    }

    void Update()
    {
        if(CutPaper.objectIsPlaced)
        {
            cutButton.SetActive(true);
            startButton.SetActive(false);
        }
    }
}
