using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterUpdate : MonoBehaviour
{
    public GameParameters gameParam;
    public Text scoreCounterText;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int score = this.gameParam.score;
        this.scoreCounterText.text = "Score: " + score;
    }
}
