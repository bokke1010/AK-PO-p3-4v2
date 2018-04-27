using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highlightPointer : MonoBehaviour {
	public locationManager pointLoc;
	private Vector2 screenCenter;
	private Camera mainCamera;
	public bool selected;
	public float baseScale;
	private Transform selectormodel;
	public DataLoader gamecontroller;

	// button related
	private Text[] output;
	public GameObject buttonPrefab;
	private GameObject[] buttons;
	public Material correctMat;
	public Material incorrectMat;
	public bool active;
	public cardSpawner cardsource;

	// Use this for initialization
	void Start () {
		active = true;
		mainCamera = Camera.main;
		transform.localPosition = new Vector3 (pointLoc.coords [0], 0f, pointLoc.coords [1]);
		screenCenter = new Vector2 (Screen.width, Screen.height)/2;
		Debug.Log (screenCenter);
		selectormodel = transform.GetChild (0).transform;
		baseScale = selectormodel.localScale.x;
		gamecontroller = FindObjectOfType<DataLoader> ();
		cardsource = FindObjectOfType<cardSpawner> ();

		// text content
		buttons = new GameObject[pointLoc.question.answer.Length];
		output = gameObject.GetComponentsInChildren<Text> ();
		output [0].text = pointLoc.placeName + "..." + pointLoc.desc;
		output [1].text = pointLoc.question.question;
		GameObject canvas = GetComponentInChildren<Canvas> ().gameObject;
		posAnswer[] allquestions = pointLoc.question.answer;
		for(int i = 0; i < allquestions.Length; i++){
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
			buttons[i] = Instantiate (buttonPrefab, buttonPos, canvas.transform.rotation, canvas.transform); // x range is from -60 to 60
			buttons[i].GetComponentInChildren<Text>().text = allquestions[i].option;
			int tempInt = i;
			buttons [i].GetComponent<Button> ().onClick.AddListener(() => buttonPressed(tempInt));
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		Vector3 mousePosR = mainCamera.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 8.5f)); //mainCamera.nearClipPlane
		Vector2 coords = new Vector2(pointLoc.coords[0], pointLoc.coords[1]);
		if (Vector2.Distance (new Vector2(mousePosR.x, mousePosR.z), coords) < 0.6f) {
			selected = true;
		} else {
			selected = false;
		}

		configureScale (selected);
	}

	void hideVisibility(bool txtVisible, bool btnVisible){
		foreach (Text txt in output) {
			txt.enabled = txtVisible;
		}
		foreach (GameObject btn in buttons) {
			btn.GetComponentInChildren<Text> ().enabled = btnVisible;
			btn.GetComponent<Image> ().enabled = btnVisible;
		}
	}

	void buttonPressed(int _answer){
		if (active) {
			cardsource.showCard ();
			if (pointLoc.question.answer [_answer].rightAnswer) {
				gamecontroller.score += 1;
				gameObject.GetComponentInChildren<MeshRenderer> ().material = correctMat;
			} else {
				Debug.Log ("incorrect :-(");
				gameObject.GetComponentInChildren<MeshRenderer> ().material = incorrectMat;
			}
			active = false;
		}
	}

	void configureScale(bool _selected){
		if (selected) {
			selectormodel.localScale = Vector3.one * 2 * baseScale;
			hideVisibility (true, active);
		} else {
			selectormodel.localScale = Vector3.one * baseScale;
			hideVisibility (false, false);
		}
	}
}