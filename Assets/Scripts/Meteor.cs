using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
        else if(coll.gameObject.tag == "Enemy")
        {
            BaseEnemy en = coll.gameObject.GetComponent<BaseEnemy>();
            en.Die();

            if (GooglePlayServices.IsAuthenticated())
            {
                GooglePlayServices.UnlockAchievement(GPGSIds.achievement_big_strategist);
                GooglePlayServices.IncrementAchievement(GPGSIds.achievement_master_strategist, 1);
            }

            Destroy(this.gameObject);
        }
    }
}
