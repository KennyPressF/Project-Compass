using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float score;
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        EventManager.EnemyDestroyed += IncreaseScore;
    }

    private void OnDisable()
    {
        EventManager.EnemyDestroyed -= IncreaseScore;
    }

    private void IncreaseScore(float scoreValue)
    {
        score += scoreValue;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
