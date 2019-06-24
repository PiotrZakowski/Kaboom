using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrenadeCollision : FallingObjectCollision
{
    private GameObject explosion;
    protected override void CollisionWithBase()
    {
        int newScore = this.gameParam.score;
        int actualLives = this.gameParam.lives - 1;
        this.gameParam.lives = actualLives;

        Vector3 position = this.transform.position;
        position += new Vector3(0, -1, 0);
        GameObject newExplosion = Instantiate(this.explosion, position, Quaternion.identity);
        this.gameParam.PlayMissedGrenadeSound();
        
        this.gameParam.ResetLevel();

        if (actualLives == 0)
        {
            PlayerPrefs.SetInt("Score", newScore);
            SceneManager.LoadScene("GameOverScene");
        }
    }

    protected override void CollisionWithDefusor()
    {
        int newScore = this.gameParam.score;
        newScore += this.gameParam.scorePerSavedGrenade;
        this.gameParam.score = newScore;
        this.gameParam.PlaySavedGrenedeSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.OnCollision(collision);
        this.gameParam.fallenGrenadesInThisRound++;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        this.explosion = Resources.Load<GameObject>("Fx Explosion Pack/Prefebs/Exploson6");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
