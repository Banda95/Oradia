using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour {

    public Toggle musicToggle;
    public Toggle serverToggle;
	// Use this for initialization
	void Start () {
        musicToggle.isOn = GameData.music;
        serverToggle.isOn = GameData.useServer;
	}

    public void Change()
    {
        GameData.music = musicToggle.isOn;
        GameData.useServer = serverToggle.isOn;

        GameData.SaveData();
    }
	
}
