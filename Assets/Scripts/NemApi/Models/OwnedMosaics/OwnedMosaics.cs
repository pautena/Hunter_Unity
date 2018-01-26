using System;
using System.Collections.Generic;

namespace NemApi.Models{
	[System.Serializable]
	public class OwnedMosaics{
		public OwnedMosaic[] data;


		override public string ToString(){
			string result=  "OwnedMosaics. size: "+data.Length+" -> ";

			foreach (OwnedMosaic mosaic in data) {
				result += mosaic.ToString () + ", ";
			}


			return result;
		}

		public OwnedMosaic[] FindMosaicsByNamespace(string nemNamespace){
			List<OwnedMosaic> ownedMosaics = new List<OwnedMosaic> ();

			foreach(OwnedMosaic ownedMosaic in data){
				if (ownedMosaic.mosaicId.namespaceId == nemNamespace) {
					ownedMosaics.Add (ownedMosaic);
				}
			}
			return ownedMosaics.ToArray ();
		}

		public void FilterMosaicsByNamespace(string nemNamespace){
			List<OwnedMosaic> filteredMosaics = new List<OwnedMosaic> ();

			foreach(OwnedMosaic ownedMosaic in data){
				if (ownedMosaic.mosaicId.namespaceId == nemNamespace) {
					filteredMosaics.Add (ownedMosaic);
				}
			}
			data = filteredMosaics.ToArray ();
		}

		public OwnedMosaic FindMosaicById(MosaicId mosaicId){
			foreach(OwnedMosaic ownedMosaic in data){
				if (ownedMosaic.mosaicId.Equals(mosaicId)) {
					return ownedMosaic;
				}
			}
			return null;
		}
	}
}

