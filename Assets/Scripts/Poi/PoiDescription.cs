using System;

namespace Poi{
	public class PoiDescription{

		public string poi_id;
		public string name;
		public string description;
		public string img_url;

		public string ToString(){
			return "poi_id: "+poi_id+", name: "+name;
		}

	}
}

