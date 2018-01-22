using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Poi;
using NemApi.Models;
using System;
using UnityEngine.Networking;

namespace NemApi{
	public class ConnectionApi : MonoBehaviour {

		public string baseUrl = "bigalice2.nem.ninja";
		public int port = 7890;
		public string nemNamespace = "hunter";
		public string address ="TBWHKDPRWQYD5JFVATPOOBJZQDFXZ3LDHYYBJHF3";

		private string mosaicDefinitionPath = "account/mosaic/definition/page";
		private string mosaicOwnerPath="account/mosaic/owned";
		private string transactionPrepareAnnounce="transaction/prepare-announce";

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void GetMosaicDefinition(Action<MosaicGroup> callback){

			string url = "http://" + baseUrl + ":" + port + "/" + mosaicDefinitionPath 
				+ "?address=" + address + "&parent=" + nemNamespace;

			ObservableWWW.Get(url)
				.Subscribe(
					x => this.OnLoadMosaicsSuccess(x,callback), // onSuccess
					ex => Debug.LogException(ex)); // onError



		}

		public void OnLoadMosaicsSuccess(string response,Action<MosaicGroup> callback){
			MosaicGroup mosaicGroup = JsonUtility.FromJson<MosaicGroup> (response);
			callback.Invoke (mosaicGroup);

		}

		public void LoadOwnedMosaics(Action<OwnedMosaic[]> callback){
			string address = "TAUYBFWNP3D26H3UEG2ED6T6DI6YMN3EGEJ3LKFE";//TODO: Remove and pick from user info
			string url = "http://" + baseUrl + ":" + port + "/" + mosaicOwnerPath
			             + "?address=" + address;


			Debug.Log ("LoadOwnedMosaics. url: "+url);

			ObservableWWW.Get(url)
				.Subscribe(
					x => this.OnLoadOwnerMosaicsSuccess(x,callback), // onSuccess
					ex => Debug.LogException(ex)); // onError
		}

		public void OnLoadOwnerMosaicsSuccess(string response,Action<OwnedMosaic[]> callback){
			Debug.Log ("response: " + response);
			OwnedMosaics responseOwnedMosaics = JsonUtility.FromJson<OwnedMosaics> (response);
			OwnedMosaic[] ownedMosaics = responseOwnedMosaics.FindMosaicsByNamespace (nemNamespace);
			callback.Invoke (ownedMosaics);
		}
	}
}
