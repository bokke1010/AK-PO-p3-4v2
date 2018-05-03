using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winTextAdder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DataLoader source = FindObjectOfType<DataLoader> ();
		Text output = FindObjectOfType<Text> ();
		output.text = "Player " + (source.playerIndex + 1) + " has won!";
	}
}
