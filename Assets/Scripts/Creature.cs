using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour 
{
	float x, y, maxSway;
    public bool movingToFood;
    public float sway, speed, maxHealth, health, vision;
	public Vector3 targetPos;
    public bool male;
    public Sprite milfSprite;
	Collider2D[] foodInVision;

	void Start () 
	{
        if(!male)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = milfSprite;
        }

        vision /= 10;
		maxSway = sway / 10;
        movingToFood = false;
        health = maxHealth;
		targetPos = NewRandomPos(); //in het begin is target random
        Movement();
        InvokeRepeating("HealthModifier", 1, 1);
		//InvokeRepeating("LookForFood",0.5f,0.5f);
	}

    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    void Movement()
	{
		//als ik niet onderweg ben, zoek eten
		if(!movingToFood)
		{
			Vector3 foodPos = LookForFood(); 

			//als er eten gevonden is (foodpos is dan dus niet (0,0,0)), zet dit als target
			if(foodPos != Vector3.zero)
			{
				targetPos = foodPos;
			}
		} 

		//geef de sway mee
		float newX = 0, newY = 0;

		if(x < targetPos.x)
		{
			newX = Mathf.Round(Random.Range(0, SwayChecker(transform.position, targetPos)) * 10) / 10;
		}
		else if (x > targetPos.x)
		{
			newX = Mathf.Round(Random.Range(0, SwayChecker(transform.position, targetPos)) * 10) / 10;
			newX *= -1;
		}
			
		if(y < targetPos.y)
		{
			newY = Mathf.Round(Random.Range(0, SwayChecker(transform.position, targetPos)) * 10) / 10;
		}
		else if (y > targetPos.y)
		{
			newY = Mathf.Round(Random.Range(0, SwayChecker(transform.position, targetPos)) * 10) / 10;
			newY *= -1;
		}

		newX += transform.position.x;
		newY += transform.position.y;

		//maak niewe kleine stap richting target met de sway
		Vector3 moveToPos = new Vector3(newX,newY,0);

		//beweeg naar target met de kleine stap
        iTween.MoveTo(gameObject, 
			iTween.Hash
			(
				"position", moveToPos,
				"speed", speed,
				"easetype", iTween.EaseType.linear,
				"oncompletetarget", this.gameObject,
				"oncomplete", "Movement"
			));

		//als je bij target bent aangekomen, zet nieuwe random pos
		if(Vector3.Distance(transform.position,targetPos) < 0.1f)
		{
			targetPos = NewRandomPos();
		}
	}

	Vector3 LookForFood()
	{
		//array met alle colliders die zich binnen vision begeven
		foodInVision =  Physics2D.OverlapCircleAll(transform.position, vision);

		if(foodInVision != null) //als er colliders gevonden zijn
		{
			foreach(var food in foodInVision)
			{
				if(food.tag == "Food") //we moeten checken of de tag food is, hij vind nml. ook zijn eigen collider
				{
					movingToFood = true; //zo ja, geef foodpos terug
					Vector3 foundFoodPos = food.transform.position;

					return foundFoodPos;
				}
			}
		}
		return Vector3.zero; //als er niets gevonden is geven we pos van (0,0,0) terug
	}

	//zorgt dat de sway nooit meer kan zijn dan de afstand tot de target
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

	//haalt de hp eraf
	void HealthModifier()
	{
		health --;

		if (health <= 0)
		{
			GameObject.Destroy(gameObject);
		}
	}

	Vector3 NewRandomPos()
	{
		float x = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
		float y = Mathf.Round(Random.Range(0f, 5f) * 10) / 10;
		Vector3 pos = new Vector3(x,y,0);
		return pos;
	}

	void Eat(GameObject food)
	{
		health += 4;
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		Destroy(food);
		foodInVision = null;
		movingToFood = false;
		targetPos = NewRandomPos();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//als een food geraakt word, geef health, destroy de food en zet foodpos op null
		if(other.gameObject.tag == "Food")
		{
			Eat(other.gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(targetPos, new Vector3(0.05f,0.05f,0));
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position,new Vector3(vision * 2, vision * 2, 0));
	}
}