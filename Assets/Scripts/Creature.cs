using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour 
{
	float x, y, maxSway;
    bool movingToFood;
    public float sway, speed, maxHealth, health, vision;
	public Vector3 randomPos, foodPos, targetPos;

	void Start () 
	{
        vision /= 10;
        movingToFood = false;
		maxSway = sway / 10;
        health = maxHealth;
		randomPos = NewRandomPos();
        foodPos = NewRandomPos();
		Movement();
        InvokeRepeating("HealthModifier", 1, 1);
	}

    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        if ((!movingToFood && Vector3.Distance(transform.position, foodPos) < vision))
        {
            iTween.Stop(gameObject);
            targetPos = foodPos;
            movingToFood = true;
            Movement();
        }
        
    }

    void HealthModifier()
    {
        health -= 2;
        if (maxHealth > 10)
        {
            maxHealth--;
        }
    }


    void Movement()
	{
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
		float newX = 0, newY = 0;

		if(x < randomPos.x)
		{
			newX = Mathf.Round(Random.Range(0, SwayChecker(transform.position, randomPos)) * 10) / 10;

		}
		else if (x > randomPos.x)
		{
			newX = Mathf.Round(Random.Range(0, SwayChecker(transform.position, randomPos)) * 10) / 10;
			newX *= -1;
		}
			
		if(y < randomPos.y)
		{
			newY = Mathf.Round(Random.Range(0, SwayChecker(transform.position, randomPos)) * 10) / 10;
		}
		else if (y > randomPos.y)
		{
			newY = Mathf.Round(Random.Range(0, SwayChecker(transform.position, randomPos)) * 10) / 10;
			newY *= -1;
		}

		newX += transform.position.x;
		newY += transform.position.y;
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = new Vector3(newX, newY, 0);
        }

        if (Vector3.Distance(transform.position, foodPos) < 0.1f)
        {
            Eat();
        }

        iTween.MoveTo(gameObject, 
			iTween.Hash
			(
				"position", targetPos,
				"speed", speed,
				"easetype", iTween.EaseType.linear,
				"oncompletetarget", this.gameObject,
				"oncomplete", "Movement"
			));

			if(Vector3.Distance(transform.position,randomPos) < 0.1f)
			{
				randomPos = NewRandomPos();
			}
	}

    void Eat()
    {
        health += 4;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        foodPos = NewRandomPos();
        movingToFood = false;
    }

	Vector3 NewRandomPos()
	{
		float x = Mathf.Round(Random.Range(0f,5f) * 10) / 10;
		float y = Mathf.Round(Random.Range(0f,5f) * 10) / 10;
		Vector3 pos = new Vector3(x,y,0);
		return pos;
	}

	float SwayChecker(Vector3 myPos, Vector3 targetPos)
	{
		float distance = Vector3.Distance(myPos, targetPos);

		if(distance < 0.1f)
		{
			return 0.1f;
		}
		else if(distance < maxSway)
		{
			return distance;
		}

		return maxSway;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawCube(foodPos, new Vector3(0.1f,0.1f,0));

	}
}