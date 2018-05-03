using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class noteManager : MonoBehaviour {

	private DataLoader controller;

	private Text currentPlayer;

	// Use this for initialization
	void Start () {
		controller = FindObjectOfType<DataLoader> ();
		currentPlayer = gameObject.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentPlayer.text = currentPlayerOutput (controller.playerIndex + 1);
	}

	private string currentPlayerOutput(int player){
		return "Speler " + player + " is aan de beurt.";
	}
}
