using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using Poi;

namespace NemApi{
	public class LoadPois : MonoBehaviour {

		public ConnectionApi nemApi;

		// Use this for initialization
		void Start () {
			Invoke ("RequestPois", 2);

		}

		// Update is called once per frame
		void Update () {

		}

		private void RequestPois(){
			nemApi.GetMosaicDefinition (this.OnGetMosaicDefinition);
		}

		public void OnGetMosaicDefinition(MosaicGroup mosaicGroup){
			new PoiManager ().UpdatePois (mosaicGroup);
		}
	}
}
