using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] float totalTime;
    public static bool timerStarted;
    public static bool timerEnded;
    private Text timerText;
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
        timerEnded = false;
        timerStarted = false;
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
