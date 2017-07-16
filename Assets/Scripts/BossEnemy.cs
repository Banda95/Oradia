using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy {

    private int chargeSeconds = 2;
    private int chargeSpeedMultiplier =7;
    private bool charging = false;

    private float chargePosition;
    private float chargeLeft;
    private float chargeRight;

    private ParticleSystem particle;

    // Use this for initialization
    void Start () {
        InitializeComponents();
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        float distLeft = this.transform.position.x - chargeLeft;
        float distRight = this.transform.position.x - chargeRight;

        chargePosition = Mathf.Abs(distLeft) < Mathf.Abs(distRight) ? chargeLeft : chargeRight;

        ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem p in particles)
        {
            if (p.transform.name == "BossParticleSystem")
            {
                particle = p;
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Debug.Log("no player");
            return;
        }
        float dist = this.transform.position.x - chargePosition;

        if(Mathf.Abs(dist)<1  && !charging)
        {
            Vector3 dir = GetXDirection();

            StartCoroutine(Charge(dir.normalized.x));
        }
        else
        {
            if(!charging)
            {
                dist = dist > 0 ? -1 : 1;
                CheckFlip(dist);
                rb2D.velocity = new Vector2(dist * currentSpeed, 0);
            }
        }

    }

    private IEnumerator Charge(float dir)
    {
        charging = true;
        particle.transform.position = this.transform.position;
        particle.Play();
        yield return new WaitForSeconds(chargeSeconds);
        particle.Stop();
        rb2D.velocity = new Vector2(dir * currentSpeed * chargeSpeedMultiplier, 0);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Enemy") && charging)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                coll.gameObject.GetComponent<BaseEnemy>().Die();

                if (GooglePlayServices.IsAuthenticated())
                {
                    GooglePlayServices.UnlockAchievement(GPGSIds.achievement_big_dodger);
                    GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_of_dodgers, 1);
                }
            }
            this.Die();
        }
    }

    public void SetChargePosition(float chargePosLeft,float chargePosRight)
    {
        chargeLeft = chargePosLeft;
        chargeRight = chargePosRight;
    }

    public override void Die()
    {
        particle.Stop();
        base.Die();
    }
}
