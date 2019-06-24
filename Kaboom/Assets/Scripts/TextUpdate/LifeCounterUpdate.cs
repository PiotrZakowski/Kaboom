using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounterUpdate : MonoBehaviour
{
    public GameParameters gameParam;
    public Text lifeCounterText;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int lives = this.gameParam.lives;
        this.lifeCounterText.text = "Lives: " + lives;
    }
}
