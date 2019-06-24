using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsHandler : MonoBehaviour
{
    public GameParameters gameParam;

    public SlowDown slowDownPickUp;
    public Flash flashPickUp;
    public SlowFalling slowFallingPickup;

    private List<PickUp> pickUpsList;
    public List<float> pickUpsProbabilities;

    public AudioSource audioSource;

    void SetDefaultValues()
    {
        this.slowDownPickUp = new SlowDown();
        this.flashPickUp = new Flash();
        this.slowFallingPickup = new SlowFalling();

        this.pickUpsList = new List<PickUp>()
        {
            slowDownPickUp,
            flashPickUp,
            slowFallingPickup
        };

        this.pickUpsProbabilities = new List<float>
        {
            0.8f, //grenade
            0.05f, //Slow Down
            0.05f, //Flash
            0.1f //Slow Falling
        };
    }

    private void CheckIfPickUpsEnded()
    {
        foreach (PickUp pickUp in pickUpsList)
            if (pickUp.CheckIfExpired())
                pickUp.Deactivate();
    }

    private void CheckPickUpsSoundRequest()
    {
        foreach (PickUp pickUp in pickUpsList)
            if (pickUp.CheckIfPlayPickUpSound())
                this.audioSource.PlayOneShot(pickUp.pickUpSound);
    }

    public void EndAllActivePickUps()
    {
        foreach (PickUp pickUp in pickUpsList)
            if (pickUp.isActivated)
                pickUp.Deactivate();
    }

    public void ValidatePickUpsProbabilities()
    {
        float sum = 0;
        foreach (float pickUpProb in pickUpsProbabilities)
            sum += pickUpProb;

        if (sum < 1.0f)
            throw new ProbabilitiesDoNotSumToOne(sum);
    }

    // Use this for initialization
    void Start()
    {
        this.SetDefaultValues();

        this.ValidatePickUpsProbabilities();
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckIfPickUpsEnded();

        this.CheckPickUpsSoundRequest();

        foreach (PickUp pickUp in pickUpsList)
            pickUp.OnUpdate();
    }
}

public class ProbabilitiesDoNotSumToOne : System.Exception
{
    float probabilitiesSum;

    public ProbabilitiesDoNotSumToOne(float probSum)
    {
        this.probabilitiesSum = probSum;
    }
}