using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    [SerializeField] GameObject messagePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject numShrunkText;
    [SerializeField] GameObject numRemainingText;
    [SerializeField] int maxShrinkNum;
    private Text score;
    private Text numShrunk;
    private Text numRemaining;

    /// <summary>
    /// Sets the score card panel inactive and the game panel active
    /// caches the text of the score, the number of times the object was shrunk
    /// and the number of times remaining to shrink object
    /// </summary>
    private void Start()
    {
        messagePanel.SetActive(false);
        gamePanel.SetActive(true);
        score = scoreText.GetComponent<Text>();
        numShrunk = numShrunkText.GetComponent<Text>();
        numRemaining = numRemaining.GetComponent<Text>();
    }


    /// <summary>
    /// Checks if the countdown timer has ended. If it has, activates the score panel and
    /// hides the game panel. Then updates the text of the scores. 
    /// </summary>
    void Update()
    {
        if (Countdown.timerEnded)
        {
            gamePanel.SetActive(false);
            messagePanel.SetActive(true);
            UpdateScore();
        }    
    }

    /// <summary>
    /// Updates the text of main score, the number of times object was shrunk and the
    /// number of times remaining to reach nanoscale. 
    /// </summary>
    void UpdateScore()
    {
        // number of times oboject was shrunk by the maximum shrink amount, then you clamp
        // the value between 0 and 1 for safe measures and lastly convert to string with dropping the decimal points. 
        score.text = (Mathf.Clamp01(ShrinkPaper.numShrinks / maxShrinkNum) * 10).ToString("0");

        // number of times the object was shrunk
        numShrunk.text = ShrinkPaper.numShrinks.ToString();

        // times remaining to reach the nanoscale
        numRemaining.text = (maxShrinkNum - ShrinkPaper.numShrinks).ToString();
    }
}
