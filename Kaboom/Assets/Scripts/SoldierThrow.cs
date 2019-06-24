using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierThrow : MonoBehaviour
{
    public GameParameters gameParam;
    public PickUpsHandler pickUpsHandler;

    #region Items

    private GameObject grenade;
    private GameObject anvil;
    private GameObject stunGrenade;
    private GameObject clock;

    #endregion

    private List<System.Action> throwItemMethodsList;

    private Vector3 SpawnItemPosition;

    private AudioClip throwSound;
    public AudioSource audioSource;

    public void ThrowSomething(Vector3 SoldierPosition)
    {
        this.SpawnItemPosition = SoldierPosition;
        this.SpawnItemPosition += new Vector3(0, 1.1f, -0.95f);

        float rand = Random.value;
        int index = 0;
        float sum=0;
        while(true)
        {
            sum += this.pickUpsHandler.pickUpsProbabilities[index];
            if(rand < sum)
                break;
            else
                index++;
        }

        if (index == 0)
            this.gameParam.grenadesThrownInThisRound++;

        this.throwItemMethodsList[index]();
        this.audioSource.PlayOneShot(this.throwSound);
    }

    private void ThrowGrenade()
    {
        GameObject newGrenade = Instantiate(this.grenade, this.SpawnItemPosition, Quaternion.identity);
    }

    private void ThrowAnvil()
    {
        GameObject newAnvil = Instantiate(this.anvil, this.SpawnItemPosition, Quaternion.identity);
    }

    private void ThrowStunGrenade()
    {
        GameObject newStunGrenade = Instantiate(this.stunGrenade, this.SpawnItemPosition, Quaternion.identity);
    }

    private void ThrowClock()
    {
        GameObject newClock = Instantiate(this.clock, this.SpawnItemPosition, Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {
        this.grenade = Resources.Load<GameObject>("Grenade pack free/m26");
        this.anvil = Resources.Load<GameObject>("Cartoon Heavy Weights/Anvil/Anvil_RigidBody");
        this.stunGrenade = Resources.Load<GameObject>("Flashbang M-84/Prefabs/flashbang_M-84");
        this.clock = Resources.Load<GameObject>("Clock/Prefabs/Clock");

        this.throwSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-34");


        this.throwItemMethodsList = new List<System.Action>
        {
            this.ThrowGrenade,
            this.ThrowAnvil,
            this.ThrowStunGrenade,
            this.ThrowClock
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
