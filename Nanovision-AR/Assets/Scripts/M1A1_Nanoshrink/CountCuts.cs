using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCuts : MonoBehaviour
{
    private Text shrinkNumText;
    private int shrinkNum;
    // Start is called before the first frame update
    void Start()
    {
        shrinkNum = 0;
        shrinkNumText = GetComponent<Text>();
        shrinkNumText.text = shrinkNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        shrinkNum = CutPaper.numShrinks;
        shrinkNumText.text = shrinkNum.ToString();
    }
}
