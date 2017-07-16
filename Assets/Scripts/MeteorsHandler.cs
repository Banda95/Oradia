using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsHandler : MonoBehaviour {

    public GameObject MeteorPrefab;
    public GameObject WarningSign;

    public float startX;
    public float endX;
    public float baseY;
    public float baseSignY;

    public float timeToSpawn= 8;
    private float remainingToSpawn;

    // Use this for initialization
    void Start () {
        remainingToSpawn = timeToSpawn;
    }
	
	// Update is called once per frame
	void Update () {
        remainingToSpawn -= Time.deltaTime;
        if (remainingToSpawn <= 0)
        {
            remainingToSpawn = timeToSpawn;

            int r = Random.Range(1, 101);
            if (r >= 60) // 2/5 of chance
            {
                StartCoroutine(SpawnMeteor());
            }
        }
    }


    private IEnumerator SpawnMeteor()
    {
        float x = Random.Range(startX, endX);
        GameObject sign = Instantiate(WarningSign, new Vector3(x, baseSignY), this.transform.rotation, this.transform);
        yield return new WaitForSeconds(2);
        Destroy(sign);
        Instantiate(MeteorPrefab, new Vector3(x, baseY), this.transform.rotation, this.transform);


    }
}
