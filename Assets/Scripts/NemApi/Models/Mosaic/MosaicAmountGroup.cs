using System;

namespace NemApi.Models{
	[System.Serializable]
	public class MosaicAmountGroup{
		public MosaicAmount[] data;

		override public string ToString(){
			string result=  "MosaicAmountGroup. size: "+data.Length+" -> ";

			foreach (MosaicAmount mosaic in data) {
				result += mosaic.ToString () + ", ";
			}


			return result;
		}

		public MosaicAmount FindById(MosaicId id){
			foreach (MosaicAmount mosaicAmount in data) {
				if(mosaicAmount.mosaicId.Equals(id)){
					return mosaicAmount;
				}
			}
			return null;
		}
	}
}

