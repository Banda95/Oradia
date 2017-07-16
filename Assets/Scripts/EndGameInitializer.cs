
using UnityEngine;
using UnityEngine.UI;

public class EndGameInitializer : MonoBehaviour {

    public Text scoreText;
    public Text timeText;
    public Text lightText;
    public Text temperatureText;

    
    void Start () {

        scoreText.text = GameData.last_score.ToString();
        timeText.text = GameData.last_time.ToString();

        lightText.text = GameData.last_light_condition;
        temperatureText.text = GameData.last_temperature_condition;

        string record = GameData.last_score + "\t" + GameData.last_time + "\t" +
                    GameData.last_light_condition + "\t" + GameData.last_temperature_condition;
       
        ProcessAchievement();
        ProcessFinalScore();

        AddToLocal(record, GameData.last_score);
    }

    //<summary>
    //Add the obtained score to the local user's result. 
    //It checks if it is greater than one of the best 3 scores and works recursive to keep the results ordered
    //</summary>
    private void AddToLocal(string record, uint score)
    {
        for (int i = 0; i < 3; i++)
        {
            string s = GameData.ReadScore(i);

            if (s != "NONE")
            {
                string[] values = s.Split('\t');
                uint savedScore =  uint.Parse(values[0]);
                if(savedScore < score)
                {
                    GameData.AddScore(i, record);
                    AddToLocal(s,savedScore);
                    break;
                }
            }
            else
            {               
                GameData.AddScore(i, record);
                break;
            }
        }
    }

    //<summary>
    // Unlocks and updates the achievements related to the game based on light, temperature and time.
    //</summary>
    private void ProcessAchievement()
    {
        if (GooglePlayServices.IsAuthenticated())
        {
            GooglePlayServices.UnlockAchievement(GPGSIds.achievement_first_time);

            if (GameData.last_light_condition == "Light")
            {
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_player_of_the_day, 1);
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_owner_of_the_day, 1);
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_of_the_day, 1);
            }
            else
            {
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_player_of_the_night, 1);
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_owner_of_the_night, 1);
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_of_the_night, 1);
            }

            if(GameData.last_temperature_condition == "Cold")
            {
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_of_ice, 1);
            }
            else if(GameData.last_temperature_condition == "Hot")
            {
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_of_fire, 1);
            }

            if(GameData.last_time_condition == 12)
            {
                GooglePlayServices.UnlockAchievement(GPGSIds.achievement_player_of_midday);
            }
            else if(GameData.last_time_condition == 24 || GameData.last_time_condition == 0)
            {
                GooglePlayServices.UnlockAchievement(GPGSIds.achievement_player_of_midnight);
            }
        }
    }

    //<summary>
    // Add the score to the global leaderboard if it is the local best result.
    //</summary>
    private void ProcessFinalScore()
    {
        if (GooglePlayServices.IsAuthenticated())
        {
            string s = GameData.ReadScore(0);
            if (s != "NONE")
            {
                string[] values = s.Split('\t');
                uint savedScore = uint.Parse(values[0]);
                if(savedScore < GameData.last_score)
                {
                    GooglePlayServices.AddScoreToLeaderboard(GPGSIds.leaderboard_global_leaderboard, GameData.last_score);
                }
            }
            else
            {
                GooglePlayServices.AddScoreToLeaderboard(GPGSIds.leaderboard_global_leaderboard, GameData.last_score);
            }
        }
    }
}
