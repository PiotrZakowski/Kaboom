using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  BACKLOG:
 *  ~ SOME 1: Nowe pociski
 *      + DONE 1.1: debuffy
 *          + DONE 1.1.1: kowadlo - chwilowe spowolnienie
 *          + DONE 1.1.2: granat blyskowy - chwilowe oslepienie
 *      ~ SOME 1.2: buffy
 *          + DONE 1.2.1: zegarek - chwilowe spowolnienie spadania
 *          - DONT 1.2.2: helm/tarcza - chwilowa niewrazliwosc na debuffy
 *  ! TODO 2: REFEAKTOR!!!!
 *      + DONE 2.1: podzielic skrypty na foldery (Movement, Collision, TextUpdate, ButtonListeners)
 *      + DONE 2.2: pododawac private i public
 *      + DONE 2.3: zmienne typu bool w ifach (==true)
 *      + DONE 2.4: czy wyrzucic sprawdzanie Debuffow do innego obiektu?
 *      - DONT 2.5: TextUpdate mozna wykluczyc
 *      + DONE 2.6: Buff&Debuffy jako klasa
 *      + DONE 2.7: Collisiony jako klasy dziedziczące po FallingObjectMovement
 *      + DONE 2.8: Porzadek z this
 *      + DONE 2.9: Rozbic obliczenia na kilka linii (SoldierRandomMovement)
 *      ! TODO 2.10: Komentarze
 *      ! TODO 2.11: Przeniesc movement itemow z FallingObjectCollision do FallingObjectMovement 
 *  ! TODO 3: w unity z Imported wyrzucic co nie jest potrzebne
 *  + DONE 4: przemyslec czy cutFloatToDecimal w SoldierRandomMovement jest potrzebna (np. decimal random )
 *  - DONT 5: animacje dla żołnierza
 *  + DONE 6: ochrona w rankingu gdy poda sie zbyt dlugi nick 
 *  + DONE 7: pousuwac Debug logi
 *  ! TODO 8: dzwiek
 *  
 *  **********************
 *  + DONE 9: zamiana miejsc defusorow
 *  ! TODO 10: itemy spadaja pod katem
 *      ! TODO 10.1: itemy odbijaj sie od konca greenScreena - dodac na scenie jakies sciany
 */

public class GameParameters : MonoBehaviour
{
    public PickUpsHandler pickUpsHandler;

    #region Variables

    #region HorizontalMovementVariables

    public int deltaTimeRatio;
    public float objectMovementSpeed;
    public float objectMovementSpeedIterator;
    public float objectFallingSpeed;
    public float defusorSpeedModificator;

    #endregion

    #region ItemThrowVariables

    public float timePerThrow;
    public float timePerThrowRatio;

    #endregion

    #region GrenadeVariables

    public int grenadesPerRound;
    public int grenadesPerRoundIterator;
    public int grenadesThrownInThisRound;
    public int fallenGrenadesInThisRound;
    public bool playerMissedGrenade;

    #endregion

    #region PlayerVariables

    public int lives;
    public int level;
    public int score;
    public int scorePerSavedGrenade;
    public int scorePerSavedGrenadeIterator;

    #endregion

    public float soldierShakeProbability;

    private const float resetTimeWait = 3.0f;

    private AudioClip explosionSound;
    private AudioClip savedGrenadeSound;
    public AudioSource audioSource;
    

    #endregion

    private void SetDefaultValues()
    {
        this.deltaTimeRatio = 5;
        this.objectMovementSpeed = 0.1f;
        this.objectMovementSpeedIterator = 0.05f;
        this.objectFallingSpeed = 0.2f;
        this.defusorSpeedModificator = 1.2f;

        this.timePerThrow = 2.0f;
        this.timePerThrowRatio = 0.8f;

        this.grenadesPerRound = 6;
        this.grenadesPerRoundIterator = 4;
        this.grenadesThrownInThisRound = 0;
        this.fallenGrenadesInThisRound = 0;
        this.playerMissedGrenade = false;

        this.lives = 3;
        this.level = 1;
        this.score = 0;
        this.scorePerSavedGrenade = 100;
        this.scorePerSavedGrenadeIterator = 100;

        this.soldierShakeProbability = 0.25f+(Random.value/2.0f);
    }

    public void SetNextLevel()
    {
        this.pickUpsHandler.EndAllActivePickUps();

        this.grenadesThrownInThisRound = 0;
        this.fallenGrenadesInThisRound = 0;
        this.level++;
        this.objectMovementSpeed += this.objectMovementSpeedIterator;
        this.timePerThrow *= this.timePerThrowRatio;
        this.grenadesPerRound += this.grenadesPerRoundIterator;
        this.scorePerSavedGrenade += this.scorePerSavedGrenadeIterator;
        this.soldierShakeProbability = 0.25f + (Random.value / 2.0f);
    }

    private IEnumerator ResetDelay()
    {
        yield return new WaitForSecondsRealtime(resetTimeWait);
        this.fallenGrenadesInThisRound = 0;
        this.grenadesThrownInThisRound = 0;
        this.playerMissedGrenade = false;
    }

    public void ResetLevel()
    {
        this.pickUpsHandler.EndAllActivePickUps();
        

        this.playerMissedGrenade = true;
        StartCoroutine(ResetDelay());
    }

    public void PlayMissedGrenadeSound()
    {
        this.audioSource.PlayOneShot(this.explosionSound);
    }

    public void PlaySavedGrenedeSound()
    {
        this.audioSource.PlayOneShot(this.savedGrenadeSound);
    }

    // Use this for initialization
    void Start()
    {
        //to testing game in low fps environment
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 1;

        //to reseting ranking
        //PlayerPrefs.DeleteAll();

        this.SetDefaultValues();

        this.explosionSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-48");
        this.savedGrenadeSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-44");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("grenades: "+this.fallenGrenadesInThisRound+":"+this.grenadesThrownInThisRound+"/"+this.grenadesPerRound +
            " OMS: " + this.objectMovementSpeed + 
            " DSM: " + this.defusorSpeedModificator + 
            " OFS: " + this.objectFallingSpeed);
    }
}