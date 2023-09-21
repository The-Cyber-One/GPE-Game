using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TMP_Text scoreText, multiplierText, islandScoreText, totalScoreText;

    private int _score = 0, _totalScore = 0;
    private float _multiplier = 0;

    public void IncreaseScore(int amount)
    {
        _score += amount;
    }

    [ContextMenu(nameof(UpdateUI))]
    public void UpdateUI()
    {
        scoreText.SetText($"{_score}");
        multiplierText.SetText($"{_multiplier:0.0}x");
        islandScoreText.SetText($"{Mathf.CeilToInt(_score * _multiplier)}");
        totalScoreText.SetText($"<b><color=#888>Total score</color></b></n>{_totalScore}");
    }

    [ContextMenu(nameof(SaveIslandScore))]
    public void SaveIslandScore()
    {
        _totalScore += Mathf.CeilToInt(_score * _multiplier);
    }
}
