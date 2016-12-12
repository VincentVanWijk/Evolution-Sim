using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPopulation : MonoBehaviour 
{
	public int startPopulation = 5;
	public GameObject creature;
	Vector3 randomPos;
	void Start ()
	{
		randomPos = NewRandomPos();
		SpawnPopulation();
	}
	
	void Update () 
	{
		
	}

	void SpawnPopulation()
	{
		for(int i = 0; i < startPopulation; i++)
		{
			Instantiate(creature,randomPos,Quaternion.identity);
			randomPos = NewRandomPos();
		}
	}

	Vector3 NewRandomPos()
	{
		float x = Mathf.Round(Random.Range(0f,5f) * 10) / 10;
		float y = Mathf.Round(Random.Range(0f,5f) * 10) / 10;
		Vector3 pos = new Vector3(x,y,0);
		return pos;
	}
}
