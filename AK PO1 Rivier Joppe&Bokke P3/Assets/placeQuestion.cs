using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class placeQuestion {
	public string question;
	public posAnswer[] answer;
}


[System.Serializable]
public class posAnswer {
	public string option;
	public bool rightAnswer;
}