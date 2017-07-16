using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHandler : MonoBehaviour {

    public GameObject currentEnemyType;

    public GameObject spawnLeft;
    public GameObject spawnRight;

    private Vector3 spawnPositionRight;
    private Vector3 spawnPositionLeft;

    public GameObject chargePositionRight;
    public GameObject chargePositionLeft;

    private float chargePositionR;
    private float chargePositionL;

    public Color smartEnemyColor;

    public float baseMovementSpeed = 2f;

    public float spawnTimeSeconds = 4;
    public int startingDifficulty = 1;

    public float difficultyIncreaseTime = 10;

    private int currentDifficulty;
    private float currentSpawnTime;

    private float remainingToSpawn = 0;
    private bool increasing = false;

    private System.Random rand;

    public enum EnemyType
    {
        Minion,
        Smart,
        Boss
    }

	// Use this for initialization
	void Start () {
        currentDifficulty = startingDifficulty;
        currentSpawnTime = spawnTimeSeconds;
        rand = new System.Random(System.DateTime.UtcNow.Millisecond);

        spawnPositionLeft = spawnLeft.transform.position;
        spawnPositionRight = spawnRight.transform.position;

        chargePositionL = chargePositionLeft.transform.position.x;
        chargePositionR = chargePositionRight.transform.position.x;

        Random.InitState(System.DateTime.UtcNow.Millisecond);
    }
	
	// Update is called once per frame
	void Update () {
        if(!increasing)
        {
            StartCoroutine(IncreaseDifficulty());
        }
        remainingToSpawn -= Time.deltaTime;
        if (remainingToSpawn <= 0)
        {
            remainingToSpawn = currentSpawnTime;
            
            int r = Random.Range(0, 100) + 1;
            if (r > 85) //85
            {
                Spawn(EnemyType.Boss);
                remainingToSpawn = 3f; 
            }
            else if(r > Mathf.Max(55 - currentDifficulty,40))
            {
                Spawn(EnemyType.Smart); //Smart
            }
            else
            {
                Spawn(EnemyType.Minion);
            }

        }
	}

    private void Spawn(EnemyType type)
    {
        GameObject en = Random.value <= 0.5f ? Instantiate(currentEnemyType, spawnPositionLeft, this.transform.rotation, this.transform): 
            Instantiate(currentEnemyType, spawnPositionRight, this.transform.rotation, this.transform);

        float movementDifficulty = baseMovementSpeed + 0.3f * currentDifficulty;
        if (type == EnemyType.Boss)
        {
            BossEnemy be = en.AddComponent<BossEnemy>();
            be.SetValues(5, baseMovementSpeed);
            be.SetChargePosition(chargePositionL, chargePositionR);
        }
        else if (type == EnemyType.Smart)
        {
            SmartEnemy se = en.AddComponent<SmartEnemy>();
            se.SetValues(3, GaussianRandom(movementDifficulty, 0.90f));
            se.SetColor(smartEnemyColor);
        }
        else
        {
            MinionEnemy me = en.AddComponent<MinionEnemy>();
            me.SetValues(1, GaussianRandom(movementDifficulty, 0.90f));

        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        increasing = true;

        yield return new WaitForSeconds(difficultyIncreaseTime);
        currentDifficulty++;
        currentSpawnTime -= 0.3f;
        if (currentSpawnTime <= 1f)
        {
            currentSpawnTime = 1f;
        }
        increasing = false;
    }

    private float GaussianRandom(float mean, float stdDev)
    {
        
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
        float randNormal =
                     mean + stdDev * (float)randStdNormal; //random normal(mean,stdDev^2)

        return randNormal;
    }

    public void SetEnemy(GameObject en)
    {
        currentEnemyType = en;
    }
}
