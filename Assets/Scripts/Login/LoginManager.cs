using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models.Managers;

public class LoginManager : MonoBehaviour {

	public Slider progresSlider;
	public Canvas loginInputCanvas;
	public Button logoutButton;

	public InputField secretTokenInputField;

	public float progressVelocity=6f;


	private float progress;
	private UserManager userManager;
	private MySceneManager mySceneManager;

	void Start () {
		progress = 0.0f;
		userManager = UserManager.GetInstance ();	
		mySceneManager = new MySceneManager ();
		SetupVisibleUI ();
	}
		
	void Update () {
		if (userManager.HaveUser ()) {
			progresSlider.value = progress;

			if (progress >= 1.0f) {
				mySceneManager.StartMainScene ();
			}

			progress += progressVelocity * Time.deltaTime;
		}
	}
		
	private void SetupVisibleUI(){
		if(userManager.HaveUser()){
			SetupVisibleUI (true, false, true);
		}else{
			SetupVisibleUI (false, true, false);
		}
	}

	private void SetupVisibleUI(bool activeProgressSlider,bool activeLoginInputCanvas,bool activeLogoutButton){
		progresSlider.gameObject.SetActive (activeProgressSlider);
		loginInputCanvas.gameObject.SetActive (activeLoginInputCanvas);
		logoutButton.gameObject.SetActive (activeLogoutButton);
	}


	public void Login(){
		Debug.Log ("Login");
		string secretToken = secretTokenInputField.text;

		if (secretToken == "") {
			Debug.LogError ("secretToken is empty");
			return;
		}



		userManager.Login (secretToken);
		SetupVisibleUI ();
	}

	public void Logout(){
		Debug.Log ("Logout");
		userManager.Logout ();
		SetupVisibleUI ();
	}
}
