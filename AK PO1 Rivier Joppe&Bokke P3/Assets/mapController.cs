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
		foreach(locationManager singlePoint in data.places){
			GameObject mapPoint = Instantiate(mapPointer);
			mapPoint.GetComponent<highlightPointer>().pointLoc = singlePoint;
		}
	}
}
