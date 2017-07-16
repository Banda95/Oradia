using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{

    public GameObject heart;

    private List<GameObject> hearts;
    private float size;
    // Use this for initialization
    void OnEnable()
    {
        hearts = new List<GameObject>();
        size = heart.GetComponent<SpriteRenderer>().bounds.size.x;
        size -= size / 3f;
    }

    public void PlusOneHeart()
    {
        Vector3 position = transform.position;
        position.x += size * hearts.Count;
        hearts.Add(Instantiate(heart, position, transform.rotation, this.transform));
    }

    public void MinusOneHeart()
    {
        GameObject toRemove = hearts[hearts.Count - 1];
        if (toRemove)
        {
            hearts.Remove(toRemove);
            Destroy(toRemove);
        }
    }
}
