using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSpawner : MonoBehaviour {

	public int cards;
	public DataLoader gamecontroller;
	private Vector3 showcardLoc = new Vector3(0,4,4);
	private Quaternion showcardRot = Quaternion.Euler(0,90,-90);
	private List<Vector3> hidecardLoc = new List<Vector3>();
	private List<Quaternion> hidecardRot = new List<Quaternion> ();
	private List<Vector3> desiredPos = new List<Vector3> ();
	private List<Quaternion> desiredRot = new List<Quaternion> ();

	[SerializeField]
	public GameObject Card;
	List<GameObject> cardReference = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		gamecontroller = FindObjectOfType<DataLoader> ();
		cards = gamecontroller.maxScore;
		for(int i = 0;i<cards; i++){
			desiredPos.Add (gameObject.transform.position + new Vector3 ((float)(0.02 * (i + 0.5)), 0, 0));
			GameObject newCard = AddCard (Card, desiredPos [i], gameObject);
			hidecardLoc.Add (newCard.transform.position);
			hidecardRot.Add (newCard.transform.rotation);
			desiredRot.Add (hidecardRot[i]);
			cardReference.Add (newCard);
		}
	}

	void Update (){
		for (int i = 0; i < cards; i++) {
			Vector3 posRef = cardReference [i].transform.position;
			Quaternion rotRef = cardReference [i].transform.rotation;
			if (desiredPos [i] != posRef) {
				posRef = Vector3.Lerp (posRef, desiredPos [i], 0.2f);
				cardReference [i].transform.position = posRef;
			}
			if (desiredRot [i] != rotRef) {
				rotRef = Quaternion.Lerp (rotRef, desiredRot [i], 0.15f);
				cardReference [i].transform.rotation = rotRef;
			}
		}
	}

	public void RemoveCard(){
		cards -= 1;
		Destroy (cardReference [cards]);
		cardReference.RemoveAt (cards);
		desiredPos.RemoveAt (cards);
	}

	public void showCard(int index, bool show = true){
		if (show) {
			desiredPos [index] = showcardLoc;
			desiredRot [index] = showcardRot;
		} else {
			desiredPos [index] = hidecardLoc[index];
			desiredRot [index] = hidecardRot[index];
		}
	}

	public void fillCard(int index, string desc, string question, posAnswer[] answers,
			GameObject buttonPrefab, highlightPointer parent){
		GameObject objCanvas = cardReference[index].transform.GetChild (1).gameObject;
		Text[] output = objCanvas.GetComponentsInChildren<Text> ();
		output[0].text = desc;
		output[1].text = question;
		// NOTE: dynamically generate buttons
		GameObject[] buttons = new GameObject[answers.Length];
		for(int i = 0; i < answers.Length; i++){
			// nice circle of buttons around the object
			Vector3 buttonPos = gameObject.transform.position;
			buttonPos.x -= 0.1f;
			buttonPos.z += 0f;
			buttonPos.y += (0.6f * i) - 2f;
			// actually instantiate the button
			buttons[i] = Instantiate (buttonPrefab, buttonPos, Quaternion.Euler(0,90,0),
				objCanvas.transform); // x range is from -60 to 60
			buttons[i].GetComponentInChildren<Text>().text = answers[i].option;
			buttons[i].GetComponentInChildren<Text>().fontSize = 24;
			buttons [i].transform.localScale = Vector3.one * 0.4f;
			int tempInt = i;
			buttons [i].GetComponent<Button> ().onClick.AddListener(() => parent.buttonPressed(tempInt));
		}
	}

	public GameObject AddCard(GameObject prefab, Vector3 position, GameObject parent){
		return Instantiate (prefab, position, Quaternion.Euler(-90,0,0), parent.transform);
	}

	public void setVisible(int index, bool txt, bool btn){

	}
}