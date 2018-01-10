using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour {

	public bool enabled=true;

	public GameObject enabledGameObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//TODO: Remove this line when connected to NEM
		if (enabled) {
			Enable ();
		} else {
			Disable ();
		}		
	}


	public void Enable(){
		SetEnabled (true);
	}

	public void Disable(){
		SetEnabled (false);
	}

	private void SetEnabled(bool enabled){
		enabledGameObject.SetActive (enabled);
	}
}
