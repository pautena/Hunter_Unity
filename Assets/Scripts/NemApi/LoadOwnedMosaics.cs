using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;

namespace NemApi{

	public class LoadOwnedMosaics : MonoBehaviour {

		public NemApi nemApi;
		public MosaicUIListAdapter mosaicUIList;

		private OwnedMosaic[] ownedMosaics;
		private MosaicGroup mosaicGroup;

		// Use this for initialization
		void Start () {
			nemApi.LoadOwnedMosaics (this.OnLoadOwnedMosaicsSuccess);
			nemApi.GetMosaicDefinition (this.OnGetMosacDefinitionSuccess);
		}

		// Update is called once per frame
		void Update () {

		}

		private void OnLoadOwnedMosaicsSuccess(OwnedMosaic[] ownedMosaics){
			this.ownedMosaics = ownedMosaics;
			SendMosaics ();
			
		}

		private void OnGetMosacDefinitionSuccess(MosaicGroup MosaicGroup){
			this.mosaicGroup = MosaicGroup;
			SendMosaics ();
		}

		private void SendMosaics(){
			if (ownedMosaics != null && mosaicGroup != null) {
				mosaicUIList.SetItems (ownedMosaics,mosaicGroup);
			}
		}
	}
}
