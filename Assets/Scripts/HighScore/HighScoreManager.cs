using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private InputField inputName;

    public UnityEvent<string, int> sumbitScoreEvent;

    public void SumbitScore()
    {
        sumbitScoreEvent.Invoke(inputName.text, ScoreData.Instance.Score);
    }
    public void HasPlayedLogic(string username)
    {
        if (inputName == null)
            return;
        inputName.interactable = false;
        inputName.text = username;
    }
    public static String removeWord(String str, String word)
    {

        // Check if the word is present in string
        // If found, remove it using removeAll()
        if (str.Contains(word))
        {

            // To cover the case
            // if the word is at the
            // beginning of the string
            // or anywhere in the middle
            String tempWord = word + " ";
            str = str.Replace(tempWord, "");

            // To cover the edge case
            // if the word is at the
            // end of the string
            tempWord = " " + word;
            str = str.Replace(tempWord, "");
        }

        // Return the resultant string
        return str;
    }
}
