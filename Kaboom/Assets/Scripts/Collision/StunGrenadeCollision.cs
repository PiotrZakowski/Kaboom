using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenadeCollision : FallingObjectCollision
{
    private PickUpsHandler gamePickUpsHandler;

    protected override void CollisionWithBase()
    {
    }

    protected override void CollisionWithDefusor()
    {
        this.gamePickUpsHandler.flashPickUp.Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.OnCollision(collision);
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        this.gamePickUpsHandler = GameObject.Find("PickUpsHandler").GetComponent<PickUpsHandler>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
