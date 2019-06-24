using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDefusorMovement : MonoBehaviour
{
    public GameParameters gameParam;
    public GameObject greenScreen;

    private float defusorMovementBounds;

    private void ChangePosition(float changeValue)
    {
        Vector3 newPosition = this.transform.position;
        newPosition.x += changeValue;
        if (System.Math.Abs(newPosition.x) < this.defusorMovementBounds)
            this.transform.position = newPosition;
    }

    private void ReadControlCommand()
    {
        float positionChangeValue = this.gameParam.objectMovementSpeed
            * Time.deltaTime * this.gameParam.deltaTimeRatio
            * this.gameParam.defusorSpeedModificator;
        float mouseXPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float defusorXPosition = this.transform.position.x;
        float diffXPosition = System.Math.Abs(mouseXPosition - defusorXPosition) * Time.deltaTime * this.gameParam.deltaTimeRatio;
        if (diffXPosition < positionChangeValue)
            positionChangeValue = diffXPosition;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (mouseXPosition < defusorXPosition)
            {
                this.ChangePosition(-positionChangeValue);
            }
            else if (mouseXPosition > defusorXPosition)
            {
                this.ChangePosition(+positionChangeValue);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        float greenScreenBound = this.greenScreen.transform.lossyScale.x;

        float defusorXSize = this.transform.lossyScale.x;
        this.defusorMovementBounds = (greenScreenBound / 2.0f) - (defusorXSize / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.ReadControlCommand();
    }
}
