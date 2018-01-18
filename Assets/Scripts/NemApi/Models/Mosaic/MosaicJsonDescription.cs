using System;

namespace NemApi.Models{
	public class MosaicJsonDescription{

		public string poi_id;
		public string name;
		public string description;
		public string img_url;

		override public string ToString(){
			return "poi_id: "+poi_id+", name: "+name;
		}
	}
}

