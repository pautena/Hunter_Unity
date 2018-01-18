using System;

namespace NemApi.Models{
	[System.Serializable]
	public class OwnedMosaic{
		public int quantity;
		public MosaicId mosaicId;

		override public string ToString(){
			return "mosaicId: " + mosaicId + ", quantity: " + quantity;
		}
	}
}

