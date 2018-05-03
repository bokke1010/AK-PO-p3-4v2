using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour {

	private JsonDataInput data;
	private DataLoader jsonLocationSource;
	public GameObject mapPointer;

	// Use this for initialization
	void Start () {
		jsonLocationSource = FindObjectOfType<DataLoader> ();
		data = jsonLocationSource.locationData;
		for(int i = 0; i<data.places.Length; i++){
			GameObject mapPoint = Instantiate(mapPointer);
			highlightPointer mapPHL = mapPoint.GetComponent<highlightPointer> ();
			mapPHL.pointLoc = data.places[i];
			mapPHL.index = i;
		}
	}
}
