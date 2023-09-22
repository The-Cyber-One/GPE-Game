using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Events;
using System;
using Dan.Models;
using System.Linq;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private TextMeshProUGUI currentRankNamePlayer;
    [SerializeField]
    private TextMeshProUGUI errorText;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    TextMeshProUGUI currentHighScore;
    [SerializeField]
    private TextMeshProUGUI inputScore;

    public UnityEvent<string> hasPlayedEvent;
    public const string publicLeaderboardKey = "a56edf94067a1ec620d28c02d6048cc682b8a0c0d26c7ed3d40a87c8b4922450";
    [SerializeField] bool isFirstScene = false;
    private void Start()
    {
        inputScore?.SetText("Score: " + ScoreData.Instance.Score.ToString());
        Invoke(nameof(GetLeaderBoard), 0.5f);
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, false, new LeaderboardSearchQuery() { Take = names.Count }, ((msg) =>
        {
            //Get HighScores
            for (int i = 0; i < msg.Length; i++)
            {
                names[i].text = "#" + msg[i].Rank + " " + msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));

        LeaderboardCreator.GetPersonalEntry(publicLeaderboardKey, entry =>
        {
            if (entry.Date == 0)
                return;

            if (isFirstScene)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "AddNameScene")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                return;
            }

            hasPlayedEvent.Invoke(entry.Username);
            currentRankNamePlayer.text = $"#{entry.Rank} {entry.Username}";
            currentHighScore.text = entry.Score.ToString();

        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, msg =>
        {
            if (msg.Any(msg => msg.Username == username && !msg.IsMine()))
            {
                errorText.SetText("Name already taken");
                inputField.text = "";
                return;
            }

            //Highscore entry
            LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
            {
                GetLeaderBoard();
            }));
        });
    }

    [ContextMenu(nameof(ClearName))]
    private void ClearName()
    {
        PlayerPrefs.DeleteKey("LEADERBOARD_CREATOR___LOCAL_GUID");
    }

    public void HasPlayedGame()
    {

    }


}
