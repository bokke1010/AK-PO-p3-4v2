using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataLoader : MonoBehaviour {

	private string gameDataFileName = "test.json";
	public JsonDataInput locationData;
	public int score;
	public int maxScore;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("MainScene");
		locationData = loadGameData ();
		score = 0;
		maxScore = locationData.places.Length;

	}
	
	// Update is called once per frame
	void Update () {
		if (score == maxScore) {
			Debug.Log ("You won!");
			SceneManager.LoadScene ("youwon");
			score = 0;
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
