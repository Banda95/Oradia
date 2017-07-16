using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static string city = "CITY";
    public static string weatherDescription = "DESC";
    public static float temp= 0, rain = 0, wind = 0, clouds = 0;

    public static uint last_score = 0;
    public static uint last_time = 0;


    public static int last_time_condition = 10;
    public static string last_light_condition = "Light";
    public static string last_temperature_condition = "Normal";

    public static bool music = true;
    public static bool useServer = true;

    public static void LoadData()
    {
        music = PlayerPrefs.GetInt("Music", 1) == 1 ? true : false;
        useServer = PlayerPrefs.GetInt("Server", 1) == 1 ? true : false;
    }

    public static void SaveData()
    {
        if (music)
            PlayerPrefs.SetInt("Music", 1);
        else
            PlayerPrefs.SetInt("Music", 0);


        if (useServer)
            PlayerPrefs.SetInt("Server", 1);
        else
            PlayerPrefs.SetInt("Server", 0);

        PlayerPrefs.Save();
    }

    public static void AddScore(int num, string value)
    {
        PlayerPrefs.SetString("HIGHSCORE-"+num, value);
        PlayerPrefs.Save();
    }

    public static string ReadScore(int num)
    {
       return PlayerPrefs.GetString("HIGHSCORE-" + num, "NONE");
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
