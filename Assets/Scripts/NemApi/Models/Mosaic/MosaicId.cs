using System;

namespace NemApi.Models{
	[System.Serializable]
	public class MosaicId{
		public string namespaceId;
		public string name;

		override public string ToString(){
			return "namespaceId: " + namespaceId + ", name: " + name;
		}
		
	}
}

