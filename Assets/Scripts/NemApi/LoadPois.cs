using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using Poi;

namespace NemApi{
	public class LoadPois : MonoBehaviour {

		public ConnectionApi nemApi;

		public float delayStartRequest = 2f;
		public float repeatRequestTime = 15f;

		// Use this for initialization
		void Start () {
			Invoke ("RequestPois", delayStartRequest);
		}

		private void RequestPois(){
			nemApi.GetMosaicDefinition (this.OnGetMosaicDefinition);
			Invoke ("RequestPois", repeatRequestTime);
		}

		public void OnGetMosaicDefinition(MosaicGroup mosaicGroup){
			new PoiManager ().UpdatePois (mosaicGroup);
		}
	}
}
