using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataLoader : MonoBehaviour {

	private string gameDataFileName = "questions.json";
	public JsonDataInput locationData;
	public int playerIndex = 0;
	public int playerCount = 2;
	public int[] score;
	public int maxScore;
	public bool won = false;
	public int requiredScore;
	public GameObject activePointer;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("MainScene");
		locationData = loadGameData ();
		score = new int[playerCount]; 
		maxScore = locationData.places.Length;

	}
	
	// Update is called once per frame
	void Update () {

		requiredScore = (int)Mathf.Floor (maxScore / playerCount);
		for(int i = 0; i < playerCount; i++){
			if (score[i] >= requiredScore && !won) {
				won = true;
				SceneManager.LoadScene ("youwon");
				Debug.Log ("Player " + (i + 1).ToString() + " won!");
			}
		}
	}

	public void changePlayer(){
		playerIndex++;
		if (playerIndex >= playerCount) {
			playerIndex = 0;
		}
	}

	private JsonDataInput loadGameData(){
		string filePath = Path.Combine (Application.streamingAssetsPath, gameDataFileName);
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			JsonDataInput locationData = JsonUtility.FromJson<JsonDataInput> (dataAsJson);
			return locationData;
		}
		return null;
	}
}
