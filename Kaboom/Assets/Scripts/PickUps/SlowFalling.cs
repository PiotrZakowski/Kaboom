using UnityEngine;
using UnityEditor;

public class SlowFalling : PickUp
{
    private float speedModificator;

    protected override PickUpsType pickUpType
    {
        get { return PickUpsType.Buff; }
    }

    protected override float duration
    {
        get { return 5.0f; }
    }

    public SlowFalling() : base()
    {
        this.activationTime = Time.time;
        this.isActivated = false;
        this.speedModificator = 0.5f;

        this.pickUpSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-04");
    }

    protected override void SetChanges()
    {
        this.gameParam.objectFallingSpeed *= this.speedModificator;
    }

    protected override void DiscardChanges()
    {
        this.gameParam.objectFallingSpeed /= this.speedModificator;
    }

    public override void OnUpdate()
    {
    }
}