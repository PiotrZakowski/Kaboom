using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadesCounterUpdate : MonoBehaviour
{
    public GameParameters gameParam;
    public Text grenadesCounterText;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int grenadesInRound = this.gameParam.grenadesPerRound;
        int grenadesFallenInRound = this.gameParam.fallenGrenadesInThisRound;
        this.grenadesCounterText.text = "Grenades: " + grenadesFallenInRound +"/"+ grenadesInRound;
    }
}
