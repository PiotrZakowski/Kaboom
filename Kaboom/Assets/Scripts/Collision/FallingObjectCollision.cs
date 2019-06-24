using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallingObjectCollision : MonoBehaviour
{
    protected GameParameters gameParam;
    private GameObject greenScreen;

    protected abstract void CollisionWithBase();
    protected abstract void CollisionWithDefusor();

    private float horizontalMovementModifier;
    private float horizontalMovementBounds;

    protected void OnCollision(Collider2D collision)
    {
        if (collision.tag == "Base")
        {
            this.CollisionWithBase();
        }
        else if (collision.tag == "Defusor")
        {
            this.CollisionWithDefusor();
        }

        Object.Destroy(this.gameObject);
    }

    // Use this for initialization
    protected virtual void Start()
    {
        this.gameParam = GameObject.Find("GameParameters").GetComponent<GameParameters>();
        this.greenScreen = GameObject.Find("GreenScreen");
        this.horizontalMovementModifier = Random.Range(-0.5f, 0.5f);
        float greenScreenBounds = this.greenScreen.transform.lossyScale.x;
        this.horizontalMovementBounds = greenScreenBounds / 2.0f;
    }

    private void CheckDestroySignal()
    {
        if (this.gameParam.playerMissedGrenade)
            Object.Destroy(this.gameObject);
    }



    private void ObjectFallingMovement()
    {
        FallingObjectMovement.TestStatic();
        float positionChangeValue = this.gameParam.objectFallingSpeed * Time.deltaTime * this.gameParam.deltaTimeRatio;
        Vector3 newPosition = this.transform.position;
        newPosition.y -= positionChangeValue;
        if (System.Math.Abs(newPosition.x + (positionChangeValue * this.horizontalMovementModifier)) >= this.horizontalMovementBounds)
            this.horizontalMovementModifier *= -1;
        newPosition.x += positionChangeValue * this.horizontalMovementModifier;
        this.transform.position = newPosition;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.CheckDestroySignal();
        this.ObjectFallingMovement();
    }
}
