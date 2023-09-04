using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public int score;
    TMP_Text scoreText;

    public void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score 0";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
  
}

