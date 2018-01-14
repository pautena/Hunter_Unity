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
			return GameObject.Find (GetPoiName(id));
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

				PoiHelper poiHelper =new PoiManager().FindPoiById(description.poi_id).GetComponent<PoiHelper>();
				poiHelper.Enable(mosaic);

			}catch(ArgumentException e){
				Debug.LogWarning (e.Message+", description: "+mosaic.description);
			}catch(NullReferenceException e){
				Debug.LogWarning ("This poi haven't attached a PoiHelper component");
			}
		}

	}
}
