using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizePanelManager : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCanWinPrize(){
		Debug.Log ("OnCanWinPrize");
		//TODO: Check if can win. if true:
		OpenPanel();
	}

	public void OpenPanel(){
		animator.SetTrigger ("open");
	}

	public void ClosePanel(){
		animator.SetTrigger ("close");
	}
}
