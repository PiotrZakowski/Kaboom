using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounterUpdate : MonoBehaviour
{
    public GameParameters gameParam;
    public Text levelCounterText;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int level = this.gameParam.level;
        this.levelCounterText.text = "Level: " + level;
    }
}
