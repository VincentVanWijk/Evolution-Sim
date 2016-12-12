using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DrawGrid : MonoBehaviour {

	public float spawnRate = 1;
	public float minX, maxX, minY, maxY;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnDrawGizmos()
	{
		int count =  0;
		for (float i = minX ; i < maxX; i += 0.1f) 
		{

			float Xbegin = i - 0.05f;
			float Ybegin = minY - 0.05f;

			Vector3 beginPos = new Vector3(Xbegin,Ybegin);

			float Xend = i - 0.05f;
			float Yend = maxY - 0.05f;

			Vector3 endPos = new Vector3(Xend,Yend);

			if (count % 50 == 0)
			{
				Gizmos.color = Color.red;
			}
			else if (count % 10 == 0) 
			{
				Gizmos.color = Color.black;
			}
			else
			{
				Gizmos.color = Color.gray;
			}

			count++;

			Gizmos.DrawLine(beginPos,endPos);
		}

		count = 0;

		for (float i = minY; i < maxY; i += 0.1f) 
		{

			float Xbegin = minX - 0.05f;
			float Ybegin = i - 0.05f;

			Vector3 beginPos = new Vector3(Xbegin,Ybegin);

			float Xend = maxX - 0.05f;
			float Yend = i - 0.05f;

			Vector3 endPos = new Vector3(Xend,Yend);

			if (count % 50 == 0)
			{
				Gizmos.color = Color.red;
			}
			else if (count % 10 == 0) 
			{
				Gizmos.color = Color.black;
			}
			else
			{
				Gizmos.color = Color.gray;
			}

			count ++;
			Gizmos.DrawLine(beginPos,endPos);
		}
	}
}
