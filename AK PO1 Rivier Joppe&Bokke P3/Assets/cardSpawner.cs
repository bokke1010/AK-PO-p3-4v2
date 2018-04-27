using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSpawner : MonoBehaviour {

	public int cards;
	public DataLoader gamecontroller;
	private Vector3 showcardLoc = new Vector3(0,0,6);
	private Quaternion showcardRot = Quaternion.Euler(0,90,-90);

	[SerializeField]
	public GameObject Card;
	List<GameObject> cardReference = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		gamecontroller = FindObjectOfType<DataLoader> ();
		cards = gamecontroller.maxScore;
		for(int i = 0;i<cards; i++){
			GameObject newCard = AddCard (Card, gameObject.transform.position + new Vector3 ((float)(0.02 * (i + 0.5)), 0, 0), gameObject);
			Debug.Log (cardReference);
			cardReference.Add (newCard);
		}
	}

	public void RemoveCard(){
		cards -= 1;
		Destroy (cardReference [cards]);
		cardReference.RemoveAt (cards);
	}

	public void showCard(){
		cardReference[cards-1].transform.position = showcardLoc;
		cardReference[cards-1].transform.rotation = showcardRot;
	}

	public GameObject AddCard(GameObject prefab, Vector3 position, GameObject parent){
		return Instantiate (prefab, position, Quaternion.Euler(90,0,0), parent.transform);
	}
}