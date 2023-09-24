using Dan.Main;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private float maxMultiplier = 5f, multiplierBonusAmount = 4f;
    [SerializeField] private TMP_Text scoreText, multiplierText, islandScoreText, totalScoreText;
    [SerializeField] private AudioSource bonusAudio;
    [SerializeField] private AudioSource fullislandAudio;
    [SerializeField] private Animator animator;

    private int _score = 0, _totalScore = 0;
    private float _multiplier = 0, _bonusMultiplier = 0, _filledPercentage = 0;

    private void OnValidate()
    {
        if (bonusAudio ==null)
            bonusAudio = GetComponent<AudioSource>();
    }

    public void IncreaseBonus()
    {
        _bonusMultiplier += multiplierBonusAmount;
        bonusAudio.Play();
    }

    public void IncreaseScore(int amount, float filledPercentage)
    {
        _score += amount;
        _multiplier = maxMultiplier * filledPercentage + _bonusMultiplier;
        _filledPercentage = filledPercentage;
        UpdateUI();
    }

    [ContextMenu(nameof(UpdateUI))]
    private void UpdateUI()
    {
        scoreText.SetText(_score.ToString());
        multiplierText.SetText($"{_multiplier:0.0}x");
        islandScoreText.SetText(Mathf.FloorToInt(_score * _multiplier).ToString());
        totalScoreText.SetText($"<b><color=#fbb03b>Total score</color></b>\n{_totalScore}");
    }

    [ContextMenu(nameof(SaveIslandScore))]
    public void SaveIslandScore()
    {
        if (_filledPercentage == 1)
        {
            animator.SetTrigger("ShouldPlay");
            fullislandAudio.Play();
        }
        _filledPercentage = 0;
        _totalScore += Mathf.FloorToInt(_score * _multiplier);
        _score = 0; 
        _multiplier = _bonusMultiplier = 0;
        UpdateUI();
        ScoreData.Instance.Score = _totalScore;
    }
}
