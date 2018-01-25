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

		private MosaicGroup mosaicGroup;
		private MosaicAmountGroup mosaicAmountGroup;

		// Use this for initialization
		void Start () {
			Invoke ("RequestPois", delayStartRequest);
		}

		private void RequestPois(){
			nemApi.GetMosaicDefinition (this.OnGetMosaicDefinitionSuccess);
			Invoke ("RequestPois", repeatRequestTime);
		}

		public void OnGetMosaicDefinitionSuccess(MosaicGroup mosaicGroup){
			this.mosaicGroup = mosaicGroup;
			nemApi.GetMosaicsAmount (this.OnGetMosaicsAmountSuccess);
		}

		public void OnGetMosaicsAmountSuccess(MosaicAmountGroup mosaicAmountGroup){
			this.mosaicAmountGroup = mosaicAmountGroup;
			new PoiManager ().UpdatePois (mosaicGroup,mosaicAmountGroup,nemApi.network);
		}
	}
}
