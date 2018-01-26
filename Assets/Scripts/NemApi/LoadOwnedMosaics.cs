using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using Models;
using Models.Managers;

namespace NemApi{

	public class LoadOwnedMosaics : MonoBehaviour {

		public ConnectionApi nemApi;
		public MosaicUIListAdapter mosaicUIList;

		private OwnedMosaics ownedMosaics;
		private MosaicGroup mosaicGroup;
		private User user;

		// Use this for initialization
		void Start () {
			user = UserManager.GetInstance().GetUser();
			string address = user.GetAddress (nemApi.network);
			nemApi.LoadOwnedMosaics (address,this.OnLoadOwnedMosaicsSuccess);
			nemApi.GetMosaicDefinition (this.OnGetMosacDefinitionSuccess);
		}

		// Update is called once per frame
		void Update () {

		}

		private void OnLoadOwnedMosaicsSuccess(OwnedMosaics ownedMosaics){
			this.ownedMosaics = ownedMosaics;
			SendMosaics ();
			
		}

		private void OnGetMosacDefinitionSuccess(MosaicGroup MosaicGroup){
			this.mosaicGroup = MosaicGroup;
			SendMosaics ();
		}

		private void SendMosaics(){
			if (ownedMosaics != null && mosaicGroup != null) {
				mosaicUIList.SetItems (ownedMosaics.data,mosaicGroup);
			}
		}
	}
}
