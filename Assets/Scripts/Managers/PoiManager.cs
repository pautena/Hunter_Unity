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

		public void UpdatePois(MosaicGroup mosaicGroup,MosaicAmountGroup mosaicAmountGroup,byte network){

			foreach (Mosaic mosaic in mosaicGroup.data) {
				MosaicAmount mosaicAmount = mosaicAmountGroup.FindById (mosaic.id);
				if (mosaicAmount != null) {
					UpdatePoi (mosaic, mosaicAmount, network);
				}
			}
		}

		public void UpdatePoi(Mosaic mosaic,MosaicAmount mosaicAmount,byte network){
			try{
				MosaicJsonDescription description = mosaic.GetJsonDescription();

				GameObject poiGameObject = new PoiManager().FindPoiById(description.poi_id);

				if(poiGameObject!=null){
					PoiHelper poiHelper =poiGameObject.GetComponent<PoiHelper>();

					if (mosaicAmount.quantity > 0){
						poiHelper.SetMosaic(mosaic,network);
						poiHelper.SetQuantity(mosaicAmount.quantity);
					}else{
						poiHelper.Disable();
					}

				}else{
					Debug.LogError("poi "+description.poi_id+" not found");
				}

			}catch(ArgumentException){}
		}
	}
}
