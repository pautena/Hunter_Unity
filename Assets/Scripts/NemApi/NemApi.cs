using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Poi;
using NemApi.Models;

namespace NemApi{
	public class NemApi : MonoBehaviour {

		public string baseUrl = "bigalice2.nem.ninja";
		public int port = 7890;
		public string nemNamespace = "hunter";
		public string address ="TBWHKDPRWQYD5JFVATPOOBJZQDFXZ3LDHYYBJHF3";

		private string mosaicDefinitionPath = "account/mosaic/definition/page";

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void GetMosaicDefinition(){

			string url = "http://" + baseUrl + ":" + port + "/" + mosaicDefinitionPath 
				+ "?address=" + address + "&parent=" + nemNamespace;

			ObservableWWW.Get(url)
				.Subscribe(
					this.OnLoadMosaicsSuccess, // onSuccess
					ex => Debug.LogException(ex)); // onError



		}

		public void OnLoadMosaicsSuccess(string response){
			MosaicGroup mosaicGroup = JsonUtility.FromJson<MosaicGroup> (response);
			new PoiManager ().UpdatePois (mosaicGroup);

		}
	}
}
