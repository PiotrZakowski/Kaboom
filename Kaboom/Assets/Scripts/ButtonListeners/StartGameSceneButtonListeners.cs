using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameSceneButtonListeners : MonoBehaviour
{
    public Button startGameButton;
    public Button checkRankingButton;
    public Button exitButton;

    private void StartGameButton_OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void CheckRankingButton_OnClick()
    {
        SceneManager.LoadScene("RankingScene");
    }

    private void ExitButton_OnClick()
    {
        Application.Quit();
    }

    // Use this for initialization
    void Start()
    {
        this.startGameButton.onClick.AddListener(this.StartGameButton_OnClick);
        this.checkRankingButton.onClick.AddListener(this.CheckRankingButton_OnClick);
        this.exitButton.onClick.AddListener(this.ExitButton_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
