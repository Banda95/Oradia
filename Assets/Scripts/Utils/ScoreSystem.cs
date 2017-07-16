using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public Text scoreText;
    public Text timeText;

    [HideInInspector]
    public uint currentPoints = 0;
    [HideInInspector]
    public uint seconds = 0;

	// Use this for initialization
	void Start () {
        scoreText.text = currentPoints.ToString();
        timeText.text = "0";
        StartCoroutine(UpdateTimer());
    }
	

    private IEnumerator UpdateTimer()
    {
       yield return new WaitForSeconds(1);
        seconds++;
        timeText.text = seconds.ToString();
        StartCoroutine(UpdateTimer());
    }

    public void AddPoints(uint addPoints)
    {
        currentPoints += addPoints;
        scoreText.text = currentPoints.ToString();
    }
}
