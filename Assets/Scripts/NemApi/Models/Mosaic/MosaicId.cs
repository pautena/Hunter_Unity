using System;

namespace NemApi.Models{
	[System.Serializable]
	public class MosaicId{
		public string namespaceId;
		public string name;

		override public string ToString(){
			return namespaceId + ":" + name;
		}

		public override bool Equals (object obj){
			try{
				return namespaceId == ((MosaicId)obj).namespaceId && name == ((MosaicId)obj).name;
			}catch(Exception){
				return false;
			}
		}
		
	}
}

