using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour 
{
	float x, y, maxSway;
	public float sway, speed, health;
	public Vector3 randomPos;

	void Start () 
	{
		maxSway = sway / 10;

		randomPos = NewRandomPos();
		Movement();
	}

	void Update() 
	{
		x = transform.position.x;
		y = transform.position.y;
	}

	void Movement()
	{
        if (health <= 0)
        {
            return;
        }
		float newX = 0, newY = 0;
        health--;

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
		Vector3 newPos = new Vector3(newX, newY,0);

		iTween.MoveTo(gameObject, 
			iTween.Hash
			(
				"position", newPos,
				"speed", speed,
				"easetype", iTween.EaseType.linear,
				"oncompletetarget", this.gameObject,
				"oncomplete", "Movement"
			));

			if(Vector3.Distance(transform.position,randomPos) < 0.1f)
			{
				Debug.Log("ik ben er;");
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
		Gizmos.DrawCube(randomPos, new Vector3(0.1f,0.1f,0));

	}
}