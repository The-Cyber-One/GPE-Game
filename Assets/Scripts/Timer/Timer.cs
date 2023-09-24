using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Dan.Main;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private float setTime;
    private float currentTime;

    private Color startColor;
    private Color targetColor;

    [SerializeField]
    Image timerCircle;
    float elapsedTime = 0.0f;

    [SerializeField]
    Slider slider;

    [SerializeField]
    AudioSource timerSound;

    bool audioIsOn = false;


    private void Start()
    {
        startColor = Color.green;
        targetColor = Color.red;
        SetTime();
        StartCoroutine(RunTime());
    }

    private void SetTime()
    {
        slider.value = 100;
        elapsedTime = 0.0f;
        currentTime = setTime;
    }

    private IEnumerator RunTime()
    {
        while (currentTime > 0)
        {
            if (!audioIsOn && currentTime < setTime / 4)
            {
                timerSound.Play();
                audioIsOn = true;
            }
            TransitionColor();

            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("0");
            float ipValue = Mathf.Lerp(1, 0, elapsedTime / setTime);
            slider.value = ipValue;
            yield return null;
        }

        //switch to highscore screen and bring forth score
        ScoreManager.Instance.SaveIslandScore();
        LeaderboardCreator.GetPersonalEntry(HighScore.publicLeaderboardKey, entry =>
        {
            LeaderboardCreator.UploadNewEntry(HighScore.publicLeaderboardKey, entry.Username, ScoreData.Instance.Score, value =>
            {
                StartCoroutine(LoadScene());
            });
        });
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Highscores");
    }

    private void TransitionColor()
    {
        elapsedTime += Time.deltaTime;
        Color currentColor = Color.Lerp(startColor, targetColor, elapsedTime / setTime);
        timerCircle.color = currentColor;
    }
}
