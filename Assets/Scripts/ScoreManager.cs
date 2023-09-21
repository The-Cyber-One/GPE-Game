using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private float maxMultiplier = 5f, multiplierBonusAmount = 4f;
    [SerializeField] private TMP_Text scoreText, multiplierText, islandScoreText, totalScoreText;

    private int _score = 0, _totalScore = 0;
    private float _multiplier = 0, _bonusMultiplier = 0;

    public void IncreaseScore(int amount, float filledPercentage)
    {
        _score += amount;
        _multiplier = maxMultiplier * filledPercentage + _bonusMultiplier;
        UpdateUI();
    }

    [ContextMenu(nameof(UpdateUI))]
    private void UpdateUI()
    {
        scoreText.SetText($"<b>Current</b>\n{_score}");
        multiplierText.SetText($"{_multiplier:0.0}x");
        islandScoreText.SetText($"<b>Island</b>\n{Mathf.FloorToInt(_score * _multiplier)}");
        totalScoreText.SetText($"<b><color=#fbb03b>Total score</color></b>\n{_totalScore}");
    }

    [ContextMenu(nameof(SaveIslandScore))]
    public void SaveIslandScore()
    {
        _totalScore += Mathf.FloorToInt(_score * _multiplier);
        _score = 0; 
        _multiplier = _bonusMultiplier = 0;
        UpdateUI();
    }
}
