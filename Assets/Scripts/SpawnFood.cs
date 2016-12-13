using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    public int maxFood = 10;
    public GameObject food;
	public float spawnRate = 1.0f;
	GameObject[] allFood;
    Vector3 randomPos;
    void Start()
    {
		SpawnInitialFood();
		allFood = GameObject.FindGameObjectsWithTag("Food");
		InvokeRepeating("SpawnExtraFood",spawnRate,spawnRate);
    }

    void Update()
    {

    }

    void SpawnInitialFood()
    {
        for (int i = 0; i < maxFood; i++)
        {
            randomPos = NewRandomPos();
			Instantiate(food, randomPos, Quaternion.identity);
        }
    }

	void SpawnExtraFood()
	{
		if(allFood.Length < maxFood)
		{
			randomPos = NewRandomPos();
			Instantiate(food, randomPos, Quaternion.identity);
		}
		allFood = GameObject.FindGameObjectsWithTag("Food");
	}

    Vector3 NewRandomPos()
    {
        float x = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
        float y = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
        Vector3 pos = new Vector3(x, y, 0);
        return pos;
    }
}
