using System;
using UnityEngine;

namespace NemApi.Models{
	[System.Serializable]
	public class Mosaic{

		public string creator;
		public string description;
		public MosaicId id;
		public MosaicProperty[] properties;

		public string ToString(){
			return "{creator:"+creator+", description: "+description+"}";
		}

		public string GetProperty(string key){
			foreach (MosaicProperty property in properties) {
				if (property.name == key) {
					return property.value;
				}
			}
			return null;
		}

		public int GetInitialSupply(){
			return Int32.Parse(GetProperty ("initialSupply"));
		}

		public MosaicJsonDescription GetJsonDescription(){
			return JsonUtility.FromJson<MosaicJsonDescription> (description);
		}

	}
}

