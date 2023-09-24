using Dan.Main;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private float maxMultiplier = 5f, multiplierBonusAmount = 4f;
    [SerializeField] private TMP_Text scoreText, multiplierText, islandScoreText, totalScoreText;
    [SerializeField] private AudioSource bonusAudio;
    [SerializeField] private TMP_Text addedScoreTotal;
    [SerializeField] private Animator addedScoreTotalAnimation;
    int scoreToAddTotal = 0;
    int scoreToAddTotalIsland = 0;
    int totalIsland = 0;

    private int _score = 0, _totalScore = 0;
    private float _multiplier = 0, _bonusMultiplier = 0;

    private void OnValidate()
    {
        if (bonusAudio ==null)
            bonusAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

        if (scoreToAddTotal > 0)
        {
            _totalScore++;
            scoreToAddTotal--;
            UpdateUI();
        }

        if(scoreToAddTotalIsland > totalIsland)
        {
            totalIsland++;
            //scoreToAddTotalIsland--;
            UpdateUI();
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
        scoreToAddTotalIsland = Mathf.FloorToInt(_score * _multiplier);
        _multiplier = maxMultiplier * filledPercentage + _bonusMultiplier;
        UpdateUI();
    }

    [ContextMenu(nameof(UpdateUI))]
    private void UpdateUI()
    {
        scoreText.SetText(_score.ToString());
        multiplierText.SetText($"{_multiplier:0.0}x");
        islandScoreText.SetText(Mathf.FloorToInt(totalIsland).ToString());
        totalScoreText.SetText($"<b><color=#fbb03b>Total score</color></b>\n{_totalScore}");


    }

    [ContextMenu(nameof(SaveIslandScore))]
    public void SaveIslandScore()
    {
        scoreToAddTotal += Mathf.FloorToInt(_score * _multiplier);
        addedScoreTotalAnimation.SetTrigger("ScoreAdded");
        addedScoreTotal.text = scoreToAddTotal.ToString() + " +";
        _score = 0;
        totalIsland = 0;
        _multiplier = _bonusMultiplier = 0;
        UpdateUI();
        ScoreData.Instance.Score = _totalScore;
    }
}
