using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	public ImageFade imageFade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartHistoryScene(){
		Debug.Log ("StartHistoryScene");
		StartCoroutine (imageFade.FadeOut ());
		StartCoroutine(StartSceneWithDelay("HistoryScene",imageFade.duration));
	}

	private IEnumerator StartSceneWithDelay(string name, float delay){
		yield return new WaitForSeconds(delay);
		StartScene(name);
	}

	private void StartScene(string name){
		SceneManager.LoadScene(name);
	}
}
