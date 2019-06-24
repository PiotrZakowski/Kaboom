using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingSceneButtonListeners : MonoBehaviour
{
    public Button returnButton;
    public Text rankingNicks;
    public Text rankingScores;

    private void ReturnButton_OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Use this for initialization
    void Start()
    {
        this.returnButton.onClick.AddListener(this.ReturnButton_OnClick);

        for(int position=1; position<=10; position++)
        {
            string nick = PlayerPrefs.GetString("RankingNick_" + position);
            if (nick.Equals(""))
                nick = "---------------";
            this.rankingNicks.text += nick+"\n";
            this.rankingScores.text += PlayerPrefs.GetInt("RankingScore_" + position) + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
