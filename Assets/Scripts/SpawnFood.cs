using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    public int maxFood = 10;
    public GameObject food;
    Vector3 randomPos;
    void Start()
    {
        randomPos = NewRandomPos();
        Spawn();
    }

    void Update()
    {

    }

    void Spawn()
    {
        for (int i = 0; i < maxFood; i++)
        {
            Instantiate(food, randomPos, Quaternion.identity);
            randomPos = NewRandomPos();
        }
    }

    Vector3 NewRandomPos()
    {
        float x = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
        float y = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
        Vector3 pos = new Vector3(x, y, 0);
        return pos;
    }
}
