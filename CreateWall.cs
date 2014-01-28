using UnityEngine;
using System.Collections;

public class CreateWall : MonoBehaviour 
{
	private static string tagToReplay = "brick";
	public static float startTime;
	public static int numBricksY = 20;
	public static int numBricksX = 15;

	// Use this for initialization
	void Awake() 
	{
		CreateBricks(-2f);
		
	}

	void Start()
	{
		startTime = Time.realtimeSinceStartup;
	}

	public static void CreateBricks(float dz)
	{
		GameObject myBrick = Resources.Load("CubeBrick") as GameObject;
		for (int i = 0; i < numBricksY; i++)
		{
			for (int j = 0; j < numBricksX; j++)
			{
				GameObject brick = GameObject.Instantiate(myBrick) as GameObject;
				//brick.renderer.material.color = (Color.red + Color.yellow) / 2f;
				//brick.transform.localScale = new Vector3(1, 0.5f, 1f);				

				Vector3 brickPos = new Vector3(j - 6f, i / 2f, dz);
				if (i % 2 == 0)
					brick.transform.position = brickPos;
				else
					brick.transform.position = brickPos + new Vector3(0.5f, 0f, 0);

				brick.AddComponent<Rigidbody>();
				brick.rigidbody.mass = 1f;
				brick.rigidbody.drag = 0.1f;

				brick.tag = tagToReplay;
			}
		}		
	}

	public static void RemoveCompPhs()
	{
		foreach (var br in GameObject.FindGameObjectsWithTag(tagToReplay))
		{
			br.rigidbody.active = false;
			br.collider.active = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		NoInitialVelocity();
	}


	void NoInitialVelocity()
	{
		if (Time.realtimeSinceStartup - startTime < 3f)
		{
			foreach (var br in GameObject.FindGameObjectsWithTag(tagToReplay))
			{
				br.transform.rotation = Quaternion.identity;
				if (br.rigidbody != null)
				{
					br.rigidbody.velocity = Vector3.zero;
				}
			}
		}
	}	
}
