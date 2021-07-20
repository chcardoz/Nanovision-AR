using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    [SerializeField] GameObject messagePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject scoreText;

    private void Start()
    {
        messagePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Update()
    {
        if (Countdown.timerEnded)
        {
            gamePanel.SetActive(false);
            messagePanel.SetActive(true);
            UpdateScore();
        }    
    }

    void UpdateScore()
    {
        Text score = scoreText.GetComponent<Text>();
        score.text = (Mathf.Clamp01(ShrinkPaper.numShrinks / 133) * 10).ToString("0");
    }
}
