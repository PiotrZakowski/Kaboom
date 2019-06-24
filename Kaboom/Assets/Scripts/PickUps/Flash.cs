using UnityEngine;
using UnityEditor;
using System.Collections;

public class Flash : PickUp
{
    private float flashLightIntesity;
    private Light lightObject;
    private bool isFading;
    private float defaultLightIntesity;


    protected override PickUpsType pickUpType
    {
        get { return PickUpsType.Debuff; }
    }

    protected override float duration
    {
        get { return 3.0f; }
    }

    public Flash() : base()
    {
        this.activationTime = Time.time;
        this.isActivated = false;
        this.flashLightIntesity = 6.0f;
        this.defaultLightIntesity = 1.0f;
        this.isFading = false;

        this.lightObject = GameObject.Find("Directional Light").GetComponent<Light>();
        this.pickUpSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-33");
    }

    protected override void SetChanges()
    {
        this.lightObject.intensity = this.flashLightIntesity;
    }

    protected override void DiscardChanges()
    {
        this.isFading = true;
    }

    protected void Recover()
    {
        if (isFading == true)
        {
            if (this.lightObject.intensity >= this.defaultLightIntesity)
            {
                this.lightObject.intensity -= Time.deltaTime * 3;
            }
            else
            {
                this.lightObject.intensity = this.defaultLightIntesity;
                isFading = false;
            }
        }
    }

    public override void OnUpdate()
    {
        this.Recover();
    }
}