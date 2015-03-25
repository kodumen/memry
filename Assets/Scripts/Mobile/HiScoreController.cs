using UnityEngine;
using System.Collections;

public class HiScoreController : MonoBehaviour {
    public string onHiScoreLabel;
    private CardComparator cardComparator;
    private ScoreDisplay scoreDisplay;
    private int hiScore;
    private string scoreLabel;
    private bool isGameOver;

    void Start() {
        cardComparator = GameObject.FindGameObjectWithTag("card grid").GetComponent("CardComparator") as CardComparator;
        scoreDisplay = gameObject.GetComponent("ScoreDisplay") as ScoreDisplay;
        scoreLabel = scoreDisplay.label;
        if(!PlayerPrefs.HasKey("HiScore"))
            PlayerPrefs.SetInt("HiScore", 0);
        hiScore = PlayerPrefs.GetInt("HiScore");
        print(hiScore);
        isGameOver = false;
    }

    // Update is called once per frame
    void Update() {
        isGameOver = cardComparator.Moves == 0;

        if(cardComparator.Score > hiScore && !isGameOver) {
            scoreDisplay.label = scoreLabel + onHiScoreLabel;
        }
        else if(!isGameOver) {
            scoreDisplay.label = scoreLabel + @"
hi-score: " + hiScore;
        }
        else if(cardComparator.Score > hiScore && isGameOver) {
            hiScore = cardComparator.Score;
        }
    }

    void OnApplicationQuit() {
        PlayerPrefs.SetInt("HiScore", hiScore);
    }

    void OnApplicationPause(bool paused) {
        if(paused)
            PlayerPrefs.SetInt("HiScore", hiScore);
    }
}
