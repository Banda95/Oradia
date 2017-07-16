using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHandler : MonoBehaviour {

    public GameObject collectableHeart;

    private GameObject previousBonus = null;

    public float startX;
    public float endX;
    public float baseY;

    public float timeToSpawnBonus = 8;
    private float remainingToSpawn;

    void Start()
    {
        remainingToSpawn = timeToSpawnBonus;
    }

    void Update()
    {
        remainingToSpawn -= Time.deltaTime;
        if (remainingToSpawn <= 0)
        {
            remainingToSpawn = timeToSpawnBonus;

            int r = Random.Range(1, 101);
            if (r >= 60) // 2/5 of chance
            {
                SpawnHeart();
            }
        }
    }

    public void SpawnHeart()
    {
        if(previousBonus != null)
        {
            Destroy(previousBonus);            
        }
        float x = Random.Range(startX, endX);
        previousBonus = Instantiate(collectableHeart, new Vector3(x, baseY), this.transform.rotation,this.transform);
    }
}
