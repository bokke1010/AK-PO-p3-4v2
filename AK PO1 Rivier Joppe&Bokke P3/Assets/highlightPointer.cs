using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highlightPointer : MonoBehaviour {
	public locationManager pointLoc;
	// private Vector2 screenCenter;
	private Camera mainCamera;
	public bool selected;
	public int index;
	public float baseScale;
	private Transform selectormodel;
	public DataLoader gamecontroller;
	private Text[] output;
	private Vector2 coords;

	// button related
	public GameObject buttonPrefab;
	public Material correctMat;
	public Material incorrectMat;
	public bool active;
	public cardSpawner cardsource;

	// Use this for initialization
	void Start () {
		// Get all basic information
		mainCamera = Camera.main;
		active = true;
		gamecontroller = FindObjectOfType<DataLoader> ();
		cardsource = FindObjectOfType<cardSpawner> ();
		// screenCenter = new Vector2 (Screen.width, Screen.height)/2;
		// Set all necicarry variables

		coords = new Vector2( -pointLoc.coords[1], -4f - pointLoc.coords[0]);
		transform.localPosition = new Vector3 ( coords.x, 0f, coords.y); // ugly, and I don't know how, but it works
		selectormodel = transform.GetChild (0).transform;
		baseScale = selectormodel.localScale.x;
		output = gameObject.GetComponentsInChildren<Text> ();
		output [0].text = pointLoc.placeName;
		// Create related card
		cardsource.fillCard (index, pointLoc.desc, pointLoc.question.question, pointLoc.question.answer, buttonPrefab, gameObject.GetComponent<highlightPointer> ());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		Vector3 mousePosR = mainCamera.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 8.5f)); //mainCamera.nearClipPlane
		if (Vector2.Distance (new Vector2(mousePosR.x, mousePosR.z), coords) < 0.4f && Input.GetMouseButtonDown(0)) {
			selected = !selected;
			if (selected) {
				gamecontroller.activePointer = gameObject;
			}
		}
		if (gamecontroller.activePointer != gameObject) {
			selected = false;
		}
		configureScale (selected);
	}

	void hideVisibility(bool txtVisible){
		cardsource.showCard (index, txtVisible);
		output [0].enabled = txtVisible;
	}

	public void buttonPressed(int _answer){
		if (active) {
			if (pointLoc.question.answer [_answer].rightAnswer) {
				gamecontroller.score[gamecontroller.playerIndex] += 1;
				gameObject.GetComponentInChildren<MeshRenderer> ().material = correctMat;
			} else {
				Debug.Log ("incorrect :-(");
				gamecontroller.maxScore--;
				gameObject.GetComponentInChildren<MeshRenderer> ().material = incorrectMat;
			}
			Debug.Log ("score so far for player " + (gamecontroller.playerIndex + 1) + " is " + gamecontroller.score [gamecontroller.playerIndex] + " , but it needs: " + gamecontroller.requiredScore);
			gamecontroller.changePlayer ();
			active = false;
		}
	}

	void configureScale(bool _selected){
		if (selected) {
			selectormodel.localScale = Vector3.one * 2 * baseScale;
			hideVisibility (true);
		} else {
			selectormodel.localScale = Vector3.one * baseScale;
			hideVisibility (false);
		}
	}
	/*
		// text content
		//TODO: Move this all to the card
		output = gameObject.GetComponentsInChildren<Text> ();
		//TODO: cleanup text placement
		output [0].text = pointLoc.placeName + "..." + pointLoc.desc;
		output [1].text = pointLoc.question.question;
		GameObject canvas = GetComponentInChildren<Canvas> ().gameObject;
		posAnswer[] allquestions = pointLoc.question.answer;
		// NOTE: dynamically generate buttons
		buttons = new GameObject[pointLoc.question.answer.Length];
		for (int i = 0; i < allquestions.Length; i++) {
			// nice circle of buttons around the object
			Vector3 buttonPos = gameObject.transform.position;
			float angle = (float)i * (360 / (float)allquestions.Length);
			angle -= 90;
			if (angle < 0) {
				angle += 360;
			}
			buttonPos.x += 0.7f * Mathf.Sin (angle * Mathf.Deg2Rad);
			buttonPos.z += 0.7f * Mathf.Cos (angle * Mathf.Deg2Rad);
			buttonPos.y = 1;
			// actually instantiate the button
			buttons [i] = Instantiate (buttonPrefab, buttonPos, canvas.transform.rotation, canvas.transform); // x range is from -60 to 60
			buttons [i].GetComponentInChildren<Text> ().text = allquestions [i].option;
			int tempInt = i;
			buttons [i].GetComponent<Button> ().onClick.AddListener (() => buttonPressed (tempInt));
		}
	*/
}