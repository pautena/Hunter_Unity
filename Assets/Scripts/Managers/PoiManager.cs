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

		public void SetPoiName(GameObject gameOjbect,string id){
			gameOjbect.name = GetPoiName(id);
		}

		public void UpdatePois(MosaicGroup mosaicGroup){

			foreach (Mosaic mosaic in mosaicGroup.data) {
				UpdatePoi (mosaic);
			}
		}

		public void UpdatePoi(Mosaic mosaic){
			try{
				PoiDescription description = JsonUtility.FromJson<PoiDescription> (mosaic.description);

				GameObject poiGameObject = new PoiManager().FindPoiById(description.poi_id);

				if(poiGameObject!=null){
					PoiHelper poiHelper =poiGameObject.GetComponent<PoiHelper>();

					poiHelper.Enable(mosaic);
				}else{
					Debug.LogError("poi "+description.poi_id+" not found");
				}

			}catch(ArgumentException e){
				Debug.LogError (e.Message+", description: "+mosaic.description);
			}
		}

	}
}
