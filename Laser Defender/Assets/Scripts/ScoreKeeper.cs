using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public static int _score = 0;

    private Text _scoreText;

    void Awake()
    {
        _scoreText = this.GetComponent<Text>();
        Reset();
        _scoreText.text = "0";
    }


    public void Score(int point)
    {
        _score += point;

        _scoreText.text = _score.ToString();
    }

    public static int GetScore()
    {
        return _score;
    }

    public static void Reset()
    {
        _score = 0;
    }

}
