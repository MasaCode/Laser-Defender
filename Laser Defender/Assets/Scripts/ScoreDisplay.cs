using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	void Awake()
    {
        Text scoreText = this.GetComponent<Text>();
        scoreText.text = ScoreKeeper.GetScore().ToString();
        ScoreKeeper.Reset();
    }


}
