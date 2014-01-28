using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Replay : MonoBehaviour 
{
	private string tagToReplay = "brick";

	private int frameCount1 = 0;
	private int frameCount2 = 0;

	private List<List<Doubles<Vector3, Quaternion>>> doublesListList = new List<List<Doubles<Vector3, Quaternion>>>();
	private List<Doubles<Vector3, Quaternion>> initialPositions = new List<Doubles<Vector3, Quaternion>>();

	public static bool restart = false;

	GameObject[] GetGameObjectsWithTag(string tagGo)
	{
		return GameObject.FindGameObjectsWithTag(tagGo);
	}

	// Use this for initialization
	void Start ()
	{
		SaveInitialPositions(tagToReplay);
	}

	private void SaveInitialPositions(string tagGo)
	{
		foreach (var go in GetGameObjectsWithTag(tagGo))
		{
			Doubles<Vector3, Quaternion> ds;
			ds.first = go.transform.position;
			ds.second = go.transform.rotation;
			initialPositions.Add(ds);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!restart)
		{
			AddToDoublesListList(tagToReplay);
			frameCount1++;
		}
		else
		{
			if (frameCount1 > frameCount2)
			{
				ReplayGameObjects(tagToReplay);
			}
		}

		ReplayCheck(tagToReplay);
		ResetCheck(tagToReplay);
	}


	private void ReplayCheck(string tagGo)
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (!restart)
			{
				restart = true;
				ReInitPositions(tagGo);
			}
		}
	}

	private void ResetCheck(string tagGo)
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			restart = false;
			ReInitPositions(tagGo);
			CreateWall.startTime = Time.realtimeSinceStartup;
			foreach (var go in GetGameObjectsWithTag(tagGo))
			{
				if (go.rigidbody)
				{
					go.rigidbody.isKinematic = false;
				}
			}
			doublesListList = new List<List<Doubles<Vector3, Quaternion>>>();
			frameCount1 = 0;
			frameCount2 = 0;
		}
	}

	private void ReInitPositions(string tagGo)
	{
		GameObject[] goList = GetGameObjectsWithTag(tagGo);
		for (int i = 0; i < goList.Length; i++)
		{
			goList[i].transform.position = initialPositions[i].first;
			goList[i].transform.rotation = initialPositions[i].second;
			if (goList[i].rigidbody)
			{
				goList[i].rigidbody.isKinematic = true;
			}
		}
	}

	void LateUpdate()
	{
		if (restart && frameCount1 > frameCount2)
		{
			frameCount2++;
		}
	}

	private void ReplayGameObjects(string tagGo)
	{
		int i = 0;
		GameObject[] goList = GetGameObjectsWithTag(tagToReplay);

		List<Doubles<Vector3, Quaternion>> listForTheFrame = doublesListList[frameCount2];

		foreach (var goDouble in listForTheFrame)
		{
			goList[i].transform.position = goDouble.first;
			goList[i].transform.rotation = goDouble.second;
			i++;
		}
	}

	private void AddToDoublesListList(string tagGo)
	{
		doublesListList.Add(ToDoublesList(tagGo));
	}

	private List<Doubles<Vector3, Quaternion>> ToDoublesList(string tagGo)
	{
		List<Doubles<Vector3, Quaternion>> dList = new List<Doubles<Vector3, Quaternion>>();

		foreach (var go in GetGameObjectsWithTag(tagGo))
		{
			Doubles<Vector3, Quaternion> d;
			d.first = go.transform.position;
			d.second = go.transform.rotation;

			dList.Add(d);
		}

		return dList;
	}
}
