using UnityEngine;
using System.Collections;

public class Ins : MonoBehaviour {

	GUIStyle style = new GUIStyle();

	void Start()
	{
		style.fontSize = 16;
		style.normal.textColor = Color.black;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 250, 20), "W, A, S, D : Move | R: Replay, T: Rebuild wall", style);
		GUI.Label(new Rect(10, 30, 250, 20), "Mouse: Look around", style);
	}
}
