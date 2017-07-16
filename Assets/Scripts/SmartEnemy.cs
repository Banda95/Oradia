using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : BaseEnemy
{
    Color color;
    private bool fleeing = false;
    // Use this for initialization
    void Start()
    {
        InitializeComponents();
        //spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Debug.Log("no player");
            return;
        }


        Vector3 dir = GetXDirection();

        float dist = dir.magnitude;

        if (target.transform.position.y > this.transform.position.y + 0.5f)
        {
            //it's jumping near me
            if (dist < 3)
                fleeing = true;
        }
        else
        {
            fleeing = false;
        }

        if (fleeing)
        {
            CheckFlip(-dir.x);
            rb2D.velocity = new Vector2(-dir.normalized.x * currentSpeed / 2, 0);
        }
        else
        {
            CheckFlip(dir.x);
            rb2D.velocity = new Vector2(dir.normalized.x * currentSpeed, 0);
        }
    }

    public void SetColor(Color c)
    {
        color = c;
    }
}
