using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    protected int points;
    protected float currentSpeed;
    protected GameObject target;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb2D;
    protected int difficulty = 1;

    protected void InitializeComponents()
    {
        target = FindObjectOfType<Player>().gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

   protected Vector3 GetXDirection()
    {
        Vector3 dir = target.transform.position - this.transform.position;
        dir.y = 0;
        dir.z = 0;

        return dir;
    }

    protected void CheckFlip(float dirX)
    {
        if (dirX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void SetValues(int points, float speed)
    {
        this.points = points;

        currentSpeed = speed <= 1f ? 1f : speed;
        
    }

    public int GetPoints()
    {
        return points;
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
