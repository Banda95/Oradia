using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnemy : BaseEnemy {

	// Use this for initialization
	void Start () {
        InitializeComponents();
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Debug.Log("no player");
            return;
        }


        Vector3 dir = GetXDirection();

        float dist = dir.magnitude;
        if (dist <= 1)
        {
            //Reached player
            
        }
        else
        {
            CheckFlip(dir.x);
            rb2D.velocity = new Vector2(dir.normalized.x * currentSpeed, 0);
        }
	}
}
