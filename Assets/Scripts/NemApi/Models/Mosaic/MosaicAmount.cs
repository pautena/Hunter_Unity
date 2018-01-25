using System;

namespace NemApi.Models{
	[System.Serializable]
	public class MosaicAmount{
		public int quantity;
		public MosaicId mosaicId;

		override public string ToString(){
			return "{quantity:"+quantity+", mosaicId: "+mosaicId+"}";
		}
	}
}

