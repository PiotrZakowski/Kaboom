using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoldierRandomMovement : MonoBehaviour
{
    public GameParameters gameParam;
    public GameObject greenScreen;
    public GameObject defusor;

    private float greenScreenBound;
    private float defusorXSize;

    private float soldierMovementBounds;
    private bool isNewPointDrawned;
    private float drawnedPoint;

    private float soldierShakeSTD = 0.2f;

    private float lastThrowTime;

    private bool CheckIfNextLevelShouldOccur()
    {
        if (!this.gameParam.playerMissedGrenade
            && this.gameParam.grenadesPerRound == this.gameParam.grenadesThrownInThisRound
            && this.gameParam.grenadesThrownInThisRound == this.gameParam.fallenGrenadesInThisRound) // or <=
        {
            this.gameParam.SetNextLevel();
            return true;
        }
        else
            return false;
    }

    public double GaussianRandom(double mean, double stddev)
    {
        double x1 = 1 - Random.value;
        double x2 = 1 - Random.value;

        double y1 = System.Math.Sqrt(-2.0 * System.Math.Log(x1)) * System.Math.Cos(2.0 * System.Math.PI * x2);

        double result = y1 * stddev + mean;

        //GaussianRandom should only return values between [0.0 ; 1.0]
        if (result > 1.0)
            return 1.0;
        else if (result < 0.0)
            return 0.0;
        else
            return result;
    }

    private float CutFloatToDecimal(float value)
    {
        //We are trying to cut float variable to 2 numbers after comma like x.ab.
        //  value have value like x.abcdefgh... .
        //  So we multiplicate this by 100 and we get xab.cdefghij...
        //  Next we cast this value to int type - the part after comma is erased 
        //  intParse have value like xab. Finally we cast intParse back to float type and divide by 100 
        //      so we get float value like x.ab
        int intParse = (int)(value * 100.0f);
        float decimalParse = ((float)intParse) / 100.0f;

        return decimalParse;
    }

    private void DrawNewPoint()
    {
        //We are trying to find ratio with value from range[0; 1].
        //  this.transform.position.x have value from range[-this.soldierMovementBounds; +this.soldierMovementBounds].
        //  By adding this.soldierMovementBounds we shift his value range to[0; 2 * this.soldierMovementBounds].
        //  Finally we divide this range by 2 * this.soldierMovementBounds
        //      and we get range[0; 1].
        float soldierLocationToSoldierMovementBoundsRatio = (this.transform.position.x + this.soldierMovementBounds)
                / (this.soldierMovementBounds*2);
        float random;
        if (Random.value <= this.gameParam.soldierShakeProbability)
        {
            double x = this.GaussianRandom(soldierLocationToSoldierMovementBoundsRatio, this.soldierShakeSTD);
            random = this.CutFloatToDecimal((float)x);
        }
        else
            random = this.CutFloatToDecimal(Random.value);

        //We are trying to draw a point with value from range [-this.soldierMovementBounds;+this.soldierMovementBounds].
        //  random have value from range [0;1] and multiplicated with 2*this.soldierMovementBounds should have value
        //  from range [0;2*this.soldierMovementBounds]. Finally by substracting with this.soldierMovementBounds 
        //      we shift his value range to [-this.soldierMovementBounds;+this.soldierMovementBounds]
        this.drawnedPoint = (random * (this.soldierMovementBounds * 2)) - this.soldierMovementBounds;
    }

    private int CompareFloats(float a, float b, float epsilon, int precision)
    {
        //We want to compare to float values with given precision
        //  We shifting comma in both floats and epsilon precision value times in the right by multipliction
        //  Next we cast multiplicated a,b and epsilon to int type intParseA, intParseB, IntParseEpsilon
        //  Finaly we check if intParseA and intParseB are equal or value diffrence is lower than IntParseEpsilon
        //      if yes we return 0 (both are equal)
        //      if no and intParseA is greater we return 1 (first float is greater)
        //      if no and intParseB is greater we return -1 (second float is greater)
        int intParseA = (int)System.Math.Round(a * System.Math.Pow(10, precision));
        int intParseB = (int)System.Math.Round(b * System.Math.Pow(10, precision));
        int intParseEpsilon = (int)System.Math.Round(epsilon * System.Math.Pow(10, precision));

        if (intParseA == intParseB || System.Math.Abs(intParseA - intParseB) < intParseEpsilon)
            return 0;
        if (intParseA > intParseB)
            return 1;
        else
            return -1;
    }

    private void ChangePosition(float changeValue)
    {
        Vector3 newPosition = this.transform.position;
        newPosition.x += changeValue;
        if (System.Math.Abs(newPosition.x) < this.soldierMovementBounds)
            this.transform.position = newPosition;
    }

    private void CheckIfThrowItemShouldOccur()
    {
        if (!this.gameParam.playerMissedGrenade
            && Time.time >= this.lastThrowTime + this.gameParam.timePerThrow
            && this.gameParam.grenadesThrownInThisRound < this.gameParam.grenadesPerRound)
        {
            this.GetComponent<SoldierThrow>().ThrowSomething(this.transform.position);
            this.lastThrowTime = Time.time;
        }
    }

    // Use this for initialization
    void Start()
    {
        this.greenScreenBound = this.greenScreen.transform.lossyScale.x;

        this.defusorXSize = this.defusor.transform.lossyScale.x;

        this.soldierMovementBounds = (this.greenScreenBound / 2.0f) - (this.defusorXSize / 2.0f);

        this.isNewPointDrawned = false;
        this.lastThrowTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float positionChangeValue = this.gameParam.objectMovementSpeed * Time.deltaTime * this.gameParam.deltaTimeRatio;

        if (this.CheckIfNextLevelShouldOccur())
            return;

        if (!this.isNewPointDrawned)
        {
            this.DrawNewPoint();
            this.isNewPointDrawned = true;
        }
        
        Vector3 actualPosition = this.transform.position;
        if (this.CompareFloats(drawnedPoint, actualPosition.x, positionChangeValue, 2)==0)
            this.isNewPointDrawned = false; 
        else if(this.CompareFloats(drawnedPoint, actualPosition.x, positionChangeValue, 2)==-1)
            this.ChangePosition(-positionChangeValue);
        else
            this.ChangePosition(+positionChangeValue);

        this.CheckIfThrowItemShouldOccur();
    }
}
