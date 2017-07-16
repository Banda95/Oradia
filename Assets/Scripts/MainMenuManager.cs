using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public Text debugText;

    public void UnlockOne()
    {
        GooglePlayServices.UnlockAchievement(GPGSIds.achievement_first_time);
    }

    public void ShowAchievements()
    {
        GooglePlayServices.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {        
        GooglePlayServices.ShowLeaderboardUI();
    }

    public void DeletePrefs()
    {
        GameData.DeleteAll();
    }
	
}
