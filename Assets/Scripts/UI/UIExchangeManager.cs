using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using Models;
using Models.Managers;
using UnityEngine.UI;

public class UIExchangeManager : MonoBehaviour {

	public HunterApi hunterApi;
	public InputField inputMessage;
	public Animator uiExchangeAnimator;
	public MosaicUIListAdapter adapter;
	private User user;
	private Mosaic mosaic;
	private NotificationManager notificationManager;

	// Use this for initialization
	void Start () {
		user = UserManager.GetInstance ().GetUser ();
		notificationManager = GameObject.FindGameObjectWithTag ("NotificationManager").GetComponent<NotificationManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowExchangeDialog(Mosaic mosaic){
		this.mosaic = mosaic;
		Debug.Log ("RequestExchange");
		uiExchangeAnimator.SetTrigger ("Show");
	}

	public void Exchange(){
		string message = inputMessage.text;

		if (message.Length == 0) {
			notificationManager.ShowMessage("Address field is empty",false);
			return;
		}


		string privateKey = user.secretKey;
		StartCoroutine(hunterApi.Exchange(message,mosaic,privateKey,this.OnExchangeSuccess));
		uiExchangeAnimator.SetTrigger ("Hide");
	}

	private void OnExchangeSuccess(ExchangeResponse response){
		Debug.Log("response:" +response);

		if (response.IsSuccessfull ()) {
			notificationManager.ShowMessage("Exchange success",true);
			adapter.DeleteMosaic (mosaic);

		} else {
			notificationManager.ShowMessage("Error",false);
		}
	}
}
