using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] float totalTime;
    private Scrollbar scrollbar;
    private  bool timerStarted = false;
    public static bool timerEnded = false;

    private float timeRemaining;
    public float TimeRemaining
    {
        get { return timeRemaining; }
        set {
            timeRemaining = value;
            scrollbar.size = timeRemaining / totalTime;
        }
    }

    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        TimeRemaining = totalTime;
    }

    // Update is called once per frame
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
