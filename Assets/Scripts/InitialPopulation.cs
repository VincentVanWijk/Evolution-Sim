using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPopulation : MonoBehaviour 
{
	public int startPopulation = 5, playSpeed = 1;
	public GameObject creatureObj;
	public float speedMin = 1, speedMax = 3, swayMin = 1, swayMax = 3, visionMin = 1, visionMax = 3, maxHealthMin = 50, maxHealthMax = 150;
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

            GameObject c = Instantiate(creatureObj, randomPos, Quaternion.identity) as GameObject;
            Creature creature = c.GetComponent<Creature>();
            creature.speed = Random.Range(speedMin, speedMax);
            creature.sway = Random.Range(swayMin, swayMax);
            creature.vision = Random.Range(visionMin, visionMax);
			creature.maxHealth = Random.Range (maxHealthMin, maxHealthMax);
			int random = Random.Range (0, 2);
			creature.male = random == 0 ? true : false;


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
