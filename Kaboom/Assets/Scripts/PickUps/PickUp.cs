using UnityEngine;
using UnityEditor;

public abstract class PickUp
{
    public float activationTime;
    public bool isActivated;
    protected GameParameters gameParam;

    protected enum PickUpsType {Buff,Debuff};

    protected abstract PickUpsType pickUpType { get; }
    protected abstract float duration { get; }

    protected bool playPickUpSound;
    public AudioClip pickUpSound;

    public PickUp()
    {
        this.gameParam = GameObject.Find("GameParameters").GetComponent<GameParameters>();
        this.playPickUpSound = false;
    }

    protected abstract void SetChanges();

    virtual public void Activate()
    {
        this.activationTime = Time.time;
        if (!this.isActivated)
        {
            this.isActivated = true;
            this.playPickUpSound = true;
            this.SetChanges();
        }
    }

    protected abstract void DiscardChanges();

    virtual public void Deactivate()
    {
        this.isActivated = false;
        this.DiscardChanges();
    }

    public bool CheckIfExpired()
    {
        return this.isActivated && (this.activationTime + this.duration <= Time.time);
    }

    public bool CheckIfPlayPickUpSound()
    {
        if (this.playPickUpSound)
        {
            this.playPickUpSound = false;
            return true;
        }
        else
            return false;
    }

    public virtual void OnUpdate()
    {
    }
}