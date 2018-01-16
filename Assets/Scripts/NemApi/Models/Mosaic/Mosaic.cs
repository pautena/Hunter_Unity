﻿using System;

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

		public string GetInitialSupply(){
			return GetProperty ("initialSupply");
		}
	}
}

