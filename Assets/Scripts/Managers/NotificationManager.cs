using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

	public Text messageText;
	public Color successColor;
	public Color errorColor;
	public Image panelBackground;
	public float showDuration=2f;
	public Animator animator;



	public void ShowMessage(string message,bool success){
		animator.SetTrigger ("Show");
		messageText.text = message;

		if (success) {
			panelBackground.color = successColor;
		} else {
			panelBackground.color = errorColor;
		}

		Invoke ("HideMessage", showDuration);
	}

	public void HideMessage(){
		animator.SetTrigger ("Hide");
	}
}
