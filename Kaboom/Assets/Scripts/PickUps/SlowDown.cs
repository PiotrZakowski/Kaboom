using UnityEngine;
using UnityEditor;

public class SlowDown : PickUp
{
    private float speedModificator;

    protected override PickUpsType pickUpType
    {
        get { return PickUpsType.Debuff; }
    }

    protected override float duration
    {
        get { return 5.0f; }
    }

    public SlowDown() : base()
    {
        this.activationTime = Time.time;
        this.isActivated = false;
        this.speedModificator = 0.5f;
        this.pickUpSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-10");
    }

    protected override void SetChanges()
    {
        this.gameParam.defusorSpeedModificator *= this.speedModificator;
    }

    protected override void DiscardChanges()
    {
        this.gameParam.defusorSpeedModificator /= this.speedModificator;
    }

    public override void OnUpdate()
    {
    }
}