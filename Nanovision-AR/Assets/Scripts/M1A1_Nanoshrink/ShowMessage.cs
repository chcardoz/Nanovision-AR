using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [SerializeField] GameObject messagePanel;
    [SerializeField] GameObject gamePanel;
    void Update()
    {
        if (Countdown.timerEnded)
        {
            gamePanel.SetActive(false);
            messagePanel.SetActive(true);
        }    
    }
}
