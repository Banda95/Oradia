using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayServices : MonoBehaviour {
    public Text debugText;
    // Use this for initialization
    void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignIn();
	}

    void SignIn()
    {
        //PlayGamesPlatform.Instance
        Social.localUser.Authenticate((bool success) => {
            if(!success)
            {
               // debugText.text = "login not correctly";
            }

        });
    }

    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, sucess => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static bool ShowAchievementsUI()
    {
        if (Social.localUser.authenticated)
        {

            Social.ShowAchievementsUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void AddScoreToLeaderboard(string leaderboardID, long score)
    {
        Social.ReportScore(score, leaderboardID, success => { });
    }

    public static bool ShowLeaderboardUI()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }
}
