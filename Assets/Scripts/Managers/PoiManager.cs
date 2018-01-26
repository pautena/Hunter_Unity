using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using System;

namespace Poi{
	[System.Serializable]
	public class PoiManager {
		private static string POI_BASE_NAME = "Poi-";

		public string GetPoiName(string id){
			return POI_BASE_NAME + id;
		}

		public GameObject FindPoiById(string id){
			string name = GetPoiName (id);
			return GameObject.Find (name);
		}

		public GameObject FindPoiByMosaicId(MosaicId mosaicId){

			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Poi");

			foreach (GameObject gameObject in gameObjects) {
				try{
					PoiHelper poiHelper = gameObject.GetComponent<PoiHelper> ();
					Mosaic mosaic = poiHelper.GetMosaic();

					if (mosaic.id.Equals(mosaicId)){
						return gameObject;
					}
				}catch(NullReferenceException){
				}
			}
			return null;
		}

		public void SetPoiName(GameObject gameOjbect,string id){
			gameOjbect.name = GetPoiName(id);
		}

		public void UpdatePois(MosaicGroup mosaicGroup,MosaicAmountGroup mosaicAmountGroup,OwnedMosaics ownedMosaics,byte network){

			foreach (Mosaic mosaic in mosaicGroup.data) {
				MosaicAmount mosaicAmount = mosaicAmountGroup.FindById (mosaic.id);
				OwnedMosaic ownedMosaic = ownedMosaics.FindMosaicById (mosaic.id);
				if (mosaicAmount != null) {
					UpdatePoi (mosaic, mosaicAmount,ownedMosaic, network);
				}
			}
		}

		public void UpdatePoi(Mosaic mosaic,MosaicAmount mosaicAmount,OwnedMosaic ownedMosaic,byte network){
			try{
				MosaicJsonDescription description = mosaic.GetJsonDescription();

				GameObject poiGameObject = new PoiManager().FindPoiById(description.poi_id);

				if(poiGameObject!=null){
					PoiHelper poiHelper =poiGameObject.GetComponent<PoiHelper>();
					//Debug.Log("mosaic: "+mosaic.id+", quantity: "+mosaicAmount.quantity);

					/*
					 * TODO: El problema es si el quantity es < 0. No s'arriba a marcar com a owned
					 *  - En el cas de que hi hagi un altre mosaic que no s'ha agafat es mostra el altre
					 *  - En el cas de que hi hagi un owned, es substitueix.
					 * */

					if (mosaicAmount.quantity > 0 && !poiHelper.IsEnabled()){
						poiHelper.SetMosaic(mosaic,network);
						poiHelper.SetQuantity(mosaicAmount.quantity);
					}
					if(mosaicAmount.quantity == 0 && poiHelper.IsEnabled() && poiHelper.GetMosaic().id.Equals(mosaic.id)){
						poiHelper.Disable();
					}

					bool owned = ownedMosaic!=null && ownedMosaic.quantity>0;

					if(!poiHelper.IsEnabled() && owned){
						poiHelper.SetMosaic(mosaic,network);
						poiHelper.SetQuantity(0);
						poiHelper.SetOwned(true);
					}
				}else{
					Debug.LogError("poi "+description.poi_id+" not found");
				}

			}catch(ArgumentException e){
				Debug.LogError ("json error. description: " + mosaic.description);
			}
		}
	}
}
