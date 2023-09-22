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
        SceneManager.LoadScene("GameScene");
    }

    public void OnTutorialPressed()
    {
        mainMenuScreen.SetActive(false);
        tutorialScreen.SetActive(true);
    }

    public void TutorialToMM()
    {
        mainMenuScreen.SetActive(true);
        tutorialScreen.SetActive(false);
    }

    public void OnHighscorePressed()
    {
        mainMenuScreen.SetActive(false);
        highScoreScreen.SetActive(true);
    }

    public void HighscoreToMM()
    {
        mainMenuScreen.SetActive(true);
        highScoreScreen.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
