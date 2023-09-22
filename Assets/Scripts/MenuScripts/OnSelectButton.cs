using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSelectButton : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuScreen;

    [SerializeField]
    GameObject tutorialScreen;

    [SerializeField]
    GameObject highScoreScreen;

    float waitAmount = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayPressed()
    {
        StartCoroutine(LoadScreen(waitAmount, ()=>SceneManager.LoadScene("GameScene")));
    }


    private IEnumerator LoadScreen(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }



    public void OnTutorialPressed()
    {
        StartCoroutine(LoadScreen(waitAmount, () => {
            mainMenuScreen.SetActive(false);
            tutorialScreen.SetActive(true);
        }));
    }

    public void TutorialToMM()
    {
        StartCoroutine(LoadScreen(waitAmount, () => {
            mainMenuScreen.SetActive(true);
            tutorialScreen.SetActive(false);
        }));

    }

    public void OnHighscorePressed()
    {
        StartCoroutine(LoadScreen(waitAmount, () => {
            mainMenuScreen.SetActive(false);
            highScoreScreen.SetActive(true);
        }));

    }

    public void HighscoreToMM()
    {
        StartCoroutine(LoadScreen(waitAmount, () => {
            mainMenuScreen.SetActive(true);
            highScoreScreen.SetActive(false);
        }));
    }

    public void ToMainMenu()
    {
        StartCoroutine(LoadScreen(waitAmount, () => SceneManager.LoadScene("MainMenu")));
        
    }
}
