using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Events;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private TextMeshProUGUI currentUserHighScore;
    [SerializeField]
    public UnityEvent<string> hasPlayedEvent;
    private string publicLeaderboardKey = "a56edf94067a1ec620d28c02d6048cc682b8a0c0d26c7ed3d40a87c8b4922450";

    private void Start()
    {
        GetLeaderBoard();
    }
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            //Get HighScores
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
            //Get this users high score
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i].IsMine())
                {
                    hasPlayedEvent.Invoke(msg[i].Username);
                    currentUserHighScore.text = "Your Highscore:" + msg[i].Score.ToString();
                }
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        //Highscore entry
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }

    public void HasPlayedGame()
    {
    }
}
