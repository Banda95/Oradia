using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class WeatherAPIScript : MonoBehaviour {

    private string apiKey = "7acdfbedc7d0d273f379e76ddda86a38";
    // Use this for initialization
    public string city, weatherDescription;
    public float temp, tempMin, tempMax, rain, wind, clouds;

    public Text cityText;

    IEnumerator Start () {
        float lat = 44;
        float lon = 12; //London coordinates.

        if (!Input.location.isEnabledByUser)
        {
            cityText.text = "Gps not enabled." ;
           // yield break;
        }
        Input.location.Start();
        
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }
        Input.location.Stop();
        
     
        string url = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID=",lat, lon)+apiKey;

        WWW request = new WWW(url);
        yield return request;

        if(request.error == null || request.error == "")
        {
            setWeather(request.text);
            Debug.Log(request.text);
        }
        else
        {
            Debug.Log("Error:" + request.error);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void setWeather(string jsonString)
    {
        var json = JSON.Parse(jsonString);
        city = json["name"].Value;
        weatherDescription = json["weather"][0]["description"].Value;
        temp = json["main"]["temp"].AsFloat;
        tempMin = json["main"]["temp_min"].AsFloat;
        tempMax = json["main"]["temp_max"].AsFloat;
        rain = json["rain"]["3h"].AsFloat;
        clouds = json["clouds"]["all"].AsInt;
        wind = json["wind"]["speed"].AsFloat;

        cityText.text = "City: " + city;

        GameData.city = city;
        GameData.weatherDescription = weatherDescription;
        GameData.temp = temp;
        GameData.rain = rain;
        GameData.clouds = clouds;
        GameData.wind = wind;
    }
}
