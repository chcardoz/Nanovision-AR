using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCuts : MonoBehaviour
{
    private Text cutText;
    private int cutNumber;
    // Start is called before the first frame update
    void Start()
    {
        cutNumber = 0;
        cutText = GetComponent<Text>();
        cutText.text = cutNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        cutNumber = CutPaper.numCuts;
        cutText.text = cutNumber.ToString();
    }
}
