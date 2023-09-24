using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private float maxMultiplier = 5f, multiplierBonusAmount = 4f;
    [SerializeField] private TMP_Text scoreText, multiplierText, islandScoreText, totalScoreText;
    [SerializeField] private AudioSource bonusAudio, fullislandAudio;
    [SerializeField] private TMP_Text addedScoreTotal;
    [SerializeField] private Animator addedScoreTotalAnimation, droppedAnimator;
    [SerializeField] private float animationTime = 0.01f;
    [SerializeField] private int fillBonusPoints = 100;
    int _uiTotalScore = 0;
    int _uiIslandScore = 0;
    int _totalScore = 0;

    private int _score = 0, _islandScore = 0;
    private float _multiplier = 0, _bonusMultiplier = 0;

    private void OnValidate()
    {
        if (bonusAudio == null)
            bonusAudio = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        while (Application.isPlaying)
        {
            bool updateUI = false;
            if (_totalScore > _uiTotalScore)
            {
                _uiTotalScore++;
                updateUI = true;
            }

            if (_islandScore > _uiIslandScore)
            {
                _uiIslandScore++;
                updateUI = true;
            }

            if (updateUI)
            {
                UpdateUI();
            }

            yield return new WaitForSeconds(animationTime);
        }
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
        _islandScore = Mathf.FloorToInt(_score * _multiplier);

        if (filledPercentage >= 1)
        {
            droppedAnimator.SetTrigger("ShouldPlay");
            fullislandAudio.Play();
            _islandScore += fillBonusPoints;
            GridContainter.Instance.CreateIsland();
        }
    }

    [ContextMenu(nameof(UpdateUI))]
    private void UpdateUI()
    {
        scoreText.SetText(_score.ToString());
        multiplierText.SetText($"{_multiplier:0.0}x");
        islandScoreText.SetText(Mathf.FloorToInt(_uiIslandScore).ToString());
        totalScoreText.SetText($"<b><color=#fbb03b>Total score</color></b>\n{_uiTotalScore}");
    }

    [ContextMenu(nameof(SaveIslandScore))]
    public void SaveIslandScore()
    {
        _totalScore += _islandScore;

        addedScoreTotalAnimation.SetTrigger("ScoreAdded");
        addedScoreTotal.text = _islandScore.ToString() + " +";

        ScoreData.Instance.Score = _totalScore;

        _score = 0;
        _islandScore = 0;
        _multiplier = 0;
        _bonusMultiplier = 0;
        _uiIslandScore = 0;
        UpdateUI();
    }
}
