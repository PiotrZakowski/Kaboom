using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopUpUpdate : MonoBehaviour
{
    public GameParameters gameParam;
    public GameObject levelPopUp;
    public Text levelPopUpText;

    private AudioClip newLevelSound;
    public AudioSource audioSource;

    private int lastLevel;
    private float levelPopUpTimeStart;
    private float levelPopUpTime = 2.0f;

    // Use this for initialization
    void Start()
    {
        this.lastLevel = 0;
        this.newLevelSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-28");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameParam.level!=this.lastLevel)
        {
            this.lastLevel = this.gameParam.level;
            this.levelPopUp.SetActive(true);
            this.levelPopUpText.text = "LEVEL " + this.gameParam.level;
            this.levelPopUpTimeStart = Time.time;
            this.audioSource.PlayOneShot(this.newLevelSound);
        }

        if(Time.time >= this.levelPopUpTimeStart + this.levelPopUpTime)
        {
            this.levelPopUp.SetActive(false);
        }
    }
}
