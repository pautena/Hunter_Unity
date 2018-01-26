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
		public byte network = NetworkVersion.TEST_NET;

		private string mosaicDefinitionPath = "account/mosaic/definition/page";
		private string mosaicAmountPath = "account/mosaic/owned";
		private string mosaicOwnerPath="account/mosaic/owned";

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

		public void GetMosaicsAmount(Action<MosaicAmountGroup> callback){
			GetMosaicsAmount (address, callback);
		}

		public void GetMosaicsAmount(string address,Action<MosaicAmountGroup> callback){
			string url = "http://" + baseUrl + ":" + port + "/" + mosaicAmountPath + "?address=" + address;

			ObservableWWW.Get(url)
				.Subscribe(
					x => this.OnGetMosaicsAmountSuccess(x,callback), // onSuccess
					ex => Debug.LogException(ex)); // onError
		}

		public void OnGetMosaicsAmountSuccess(string response,Action<MosaicAmountGroup> callback){
			MosaicAmountGroup mosaicGroup = JsonUtility.FromJson<MosaicAmountGroup> (response);
			callback.Invoke (mosaicGroup);

		}

		public void LoadOwnedMosaics(Action<OwnedMosaics> callback){
			LoadOwnedMosaics (address, callback);
		}
			

		public void LoadOwnedMosaics(string mosaicAddress,Action<OwnedMosaics> callback){
			string url = "http://" + baseUrl + ":" + port + "/" + mosaicOwnerPath
				+ "?address=" + mosaicAddress;
			ObservableWWW.Get(url)
				.Subscribe(
					x => this.OnLoadOwnerMosaicsSuccess(x,callback), // onSuccess
					ex => Debug.LogException(ex)); // onError
		}

		public void OnLoadOwnerMosaicsSuccess(string response,Action<OwnedMosaics> callback){
			OwnedMosaics ownedMosaics = JsonUtility.FromJson<OwnedMosaics> (response);
			ownedMosaics.FilterMosaicsByNamespace (nemNamespace);
			callback.Invoke (ownedMosaics);
		}
	}
}
