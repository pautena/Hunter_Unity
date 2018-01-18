using System;

namespace NemApi.Models{
	[System.Serializable]
	public class MosaicGroup{

		public Mosaic[] data;

		public Mosaic FindMosaicById(MosaicId mosaicId){
			foreach (Mosaic mosaic in data) {
				if (mosaic.id.name == mosaicId.name && mosaic.id.namespaceId == mosaicId.namespaceId) {
					return mosaic;
				}
			}
			return null;
		}

		override public string ToString(){
			string result=  "MosaicApi. size: "+data.Length+" -> ";

			foreach (Mosaic mosaic in data) {
				result += mosaic.ToString () + ", ";
			}


			return result;
		}
	}
}

