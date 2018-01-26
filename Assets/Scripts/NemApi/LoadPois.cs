using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using Poi;
using Models;
using Models.Managers;

namespace NemApi{
	public class LoadPois : MonoBehaviour {

		public ConnectionApi nemApi;

		public float delayStartRequest = 2f;
		public float repeatRequestTime = 15f;

		private MosaicGroup mosaicGroup;
		private MosaicAmountGroup mosaicAmountGroup;
		private OwnedMosaics ownedMosaics;
		private User user;

		// Use this for initialization
		void Start () {
			Invoke ("RequestPois", delayStartRequest);
			user = UserManager.GetInstance().GetUser();
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
			string address = user.GetAddress (nemApi.network);
			nemApi.LoadOwnedMosaics (address, this.OnLoadOwnedMosaicsSuccess);
		}

		private void OnLoadOwnedMosaicsSuccess(OwnedMosaics ownedMosaics){
			this.ownedMosaics = ownedMosaics;
			new PoiManager ().UpdatePois (mosaicGroup,mosaicAmountGroup,this.ownedMosaics,nemApi.network);

		}
	}
}
