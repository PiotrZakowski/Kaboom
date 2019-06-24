using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefusorPositionChange : MonoBehaviour
{
    public GameParameters gameParam;
    public GameObject keyboardDefusor;
    public GameObject mouseDefusor;

    public AudioSource audioSource;
    private AudioClip switchDefusorsSound;

    private int lastLevel;
    private float diffInYPosition;
    private bool isMouseDefusorHigher;
    private bool isChangingPositionActive;
    private float mouseDefusorYPosition;
    private float keyboardDefusorYPosition;

    void changePositionOfDefusors()
    {
        float yStep = 0.5f * Time.deltaTime;
        Vector3 positionChangeStep = new Vector3(0.0f, yStep, 0.0f);

        if (isMouseDefusorHigher)
        {
            this.keyboardDefusor.transform.position += positionChangeStep;
            this.mouseDefusor.transform.position -= positionChangeStep;
        }
        else
        {
            this.keyboardDefusor.transform.position -= positionChangeStep;
            this.mouseDefusor.transform.position += positionChangeStep;
        }

        this.diffInYPosition -= yStep;
        if (diffInYPosition <= 0.0f)
        {
            this.keyboardDefusor.transform.position = new Vector3(
                this.keyboardDefusor.transform.position.x,
                this.mouseDefusorYPosition,
                this.keyboardDefusor.transform.position.z);
            this.mouseDefusor.transform.position = new Vector3(
                this.mouseDefusor.transform.position.x,
                this.keyboardDefusorYPosition,
                this.mouseDefusor.transform.position.z);

            this.isChangingPositionActive = false;
            this.isMouseDefusorHigher = !this.isMouseDefusorHigher;
        }
    }

    private void CheckIfSwitchPlacesBetweenDefusors()
    {
        if (this.lastLevel < this.gameParam.level
            && this.gameParam.fallenGrenadesInThisRound * 2 == this.gameParam.grenadesPerRound)
        {
            this.lastLevel= this.gameParam.level;
            this.isChangingPositionActive = true;
            this.mouseDefusorYPosition = this.mouseDefusor.transform.position.y;
            this.keyboardDefusorYPosition = this.keyboardDefusor.transform.position.y;
            this.diffInYPosition = System.Math.Abs(this.keyboardDefusorYPosition - this.mouseDefusorYPosition);
            this.audioSource.PlayOneShot(this.switchDefusorsSound);
        }
    }

    // Use this for initialization
    void Start()
    {
        this.lastLevel = 0;
        this.isMouseDefusorHigher = this.mouseDefusor.transform.position.y > this.keyboardDefusor.transform.position.y;
        this.isChangingPositionActive = false;
        this.switchDefusorsSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-47");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameParam.playerMissedGrenade)
            this.lastLevel = this.gameParam.level - 1;

        this.CheckIfSwitchPlacesBetweenDefusors();

        if (this.isChangingPositionActive)
            this.changePositionOfDefusors();
    }
}
