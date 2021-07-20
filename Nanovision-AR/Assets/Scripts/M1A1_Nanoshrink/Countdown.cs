using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private Text timerText;
    [SerializeField] float totalTime;
    public static bool timerStarted = false;
    public static bool timerEnded = false;

    private float timeRemaining;
    public float TimeRemaining
    {
        get { return timeRemaining; }
        set {
            timeRemaining = value;
            timerText.text = timeRemaining.ToString("0"); 
        }
    }

    void Start()
    {
        timerText = GetComponent<Text>();
        TimeRemaining = totalTime;
    }

    void Update()
    {
        if (timerStarted)
        {
            if(TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                timerEnded = true;
            }
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }
}
