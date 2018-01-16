using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour {

	public GameObject[] visibleGameObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hide(){
		foreach (GameObject go in visibleGameObjects) {
			go.SetActive (false);
		}
	}

	public void Show(){
		foreach (GameObject go in visibleGameObjects) {
			go.SetActive (true);
		}
	}
}
