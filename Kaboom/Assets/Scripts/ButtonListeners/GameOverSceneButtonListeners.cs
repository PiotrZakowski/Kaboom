using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameOverSceneButtonListeners : MonoBehaviour
{
    public Button restartButton;
    public Button returnToTitleMenuButton;
    public Button enterButton;

    public Text scoreText;
    
    public GameObject rankingInfo;
    public InputField inputField;

    private int positionAchieved=0;
    private int scoreAchieved;

    private AudioClip gameOverWithoutRankingSound;
    private AudioClip gameOverWithRankingSound;
    public AudioSource audioSource;

    private void RestartGameButton_OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ReturnToTitleMenuButton_OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void ShiftRanking()
    {
        for (int position = 10; position > this.positionAchieved; position--)
        {
            string nick = PlayerPrefs.GetString("RankingNick_" + (position - 1));
            int score = PlayerPrefs.GetInt("RankingScore_" + (position - 1));
            PlayerPrefs.SetString("RankingNick_" + position, nick);
            PlayerPrefs.SetInt("RankingScore_" + position, score);
        }
    }

    private void EnterButton_OnClick()
    {
        ShiftRanking();
        PlayerPrefs.SetString("RankingNick_" + this.positionAchieved, this.inputField.text);
        PlayerPrefs.SetInt("RankingScore_" + this.positionAchieved, this.scoreAchieved);
        SceneManager.LoadScene("RankingScene");
    }

    // Use this for initialization
    void Start()
    {
        this.restartButton.onClick.AddListener(this.RestartGameButton_OnClick);
        this.returnToTitleMenuButton.onClick.AddListener(this.ReturnToTitleMenuButton_OnClick);
        this.enterButton.onClick.AddListener(this.EnterButton_OnClick);

        this.gameOverWithoutRankingSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-10");
        this.gameOverWithRankingSound = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-45");

        this.rankingInfo.SetActive(false);

        this.scoreAchieved = PlayerPrefs.GetInt("Score");
        this.scoreText.text = "Your score: " + this.scoreAchieved;

        for (int position = 1; position<=10; position++)
        {
            int positionScore = PlayerPrefs.GetInt("RankingScore_" + position);
            if (positionScore < scoreAchieved)
            {
                this.rankingInfo.SetActive(true);
                this.positionAchieved = position;
                this.audioSource.PlayOneShot(this.gameOverWithRankingSound);
                break;
            }
        }

        if(this.positionAchieved == 0)
            this.audioSource.PlayOneShot(this.gameOverWithoutRankingSound);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
