using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour {

    public float baseTime = 0.2f;
    private SpriteRenderer renderer;
    private float timeToSwitch;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        timeToSwitch = baseTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeToSwitch-= Time.deltaTime;
        if(timeToSwitch <= 0)
        {
            timeToSwitch = baseTime;
            renderer.enabled = !renderer.enabled;
        }
    }
}
