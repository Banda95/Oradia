using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {

    [System.Serializable]
    public struct EnemyType
    {
       public string name;
       public GameObject prefab;
    }

    public EnemyType[] possibleEnemies;
    public GameObject enemiesHandler;

    public Color particleHot;
    public Color particleCold;
    public Color particleNormal;

    public ParticleSystem bossParticleSystem;
    public ParticleSystem rainParticleSystem;

    void Awake()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 30;
       
        if (GameData.music)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }

        if (GameData.useServer && GameData.rain > 0.2f)
        {
            rainParticleSystem.gameObject.SetActive(true);
            rainParticleSystem.Play();

            if (GooglePlayServices.IsAuthenticated())
            {
                GooglePlayServices.UnlockAchievement(GPGSIds.achievement_raining_day_isnt_it);
            }
        }
        else if (!GameData.useServer)
        {
            int r = Random.Range(0, 4);
            if (r == 0)
            {
                rainParticleSystem.gameObject.SetActive(true);
                rainParticleSystem.Play();
            }
        }
    }

    // Use this for initialization
    void Start(){
        float tempC;
        if(GameData.useServer)
            tempC = GameData.temp - 273.15f;
        else
        {
            tempC = Random.Range(0, 40);
        }
        int hour = System.DateTime.Now.Hour;
        GameData.last_time_condition = hour;

        SetEnemiesBasedOnInfo(tempC, hour);
    }

    private void SetEnemiesBasedOnInfo(float tempC, int hour)
    {
        string light = "Light";
        string temperature = "Normal";
        if (hour >= 19 || hour < 6)
        {
            light = "Dark";
        }
        var main = bossParticleSystem.main;

        if (tempC > 25)
        {
            temperature = "Hot";
            main.startColor = particleHot;
        }
        else if (tempC > 10)
        {
            temperature = "Normal";
            
            main.startColor = particleNormal;
        }
        else
        {
            temperature = "Cold";

            main.startColor = particleCold;
        }

        string name = temperature + light;

        GameData.last_temperature_condition = temperature;
        GameData.last_light_condition = light;

        foreach(EnemyType ent in possibleEnemies)
        {
            if(ent.name == name)
            {
                enemiesHandler.GetComponent<EnemiesHandler>().SetEnemy(ent.prefab);
                break;
            }
        }

    }
}
