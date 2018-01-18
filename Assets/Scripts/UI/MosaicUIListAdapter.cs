using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using UnityEngine.UI;

public class MosaicUIListAdapter : MonoBehaviour {

	public RectTransform prefab;
	public RectTransform content;
	public ScrollRect scrollView;

	private List<MosaicUIListItemView> views = new List<MosaicUIListItemView> ();



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetItems(OwnedMosaic[] mosaics,MosaicGroup mosaicGroup){
		Debug.Log("SetItems. lenght: "+mosaics.Length);

		foreach (Transform child in content) {
			Destroy (child.gameObject);
		}
		views.Clear ();

		int index = 0;
		foreach(OwnedMosaic ownedMosaic in mosaics){
			Mosaic mosaic = mosaicGroup.FindMosaicById (ownedMosaic.mosaicId);
			var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
			instance.transform.SetParent (content, false);

			MosaicUIListItemView itemView = instance.GetComponent<MosaicUIListItemView> ();

			itemView.Initialize (ownedMosaic,mosaic,index);
			views.Add (itemView);
			index++;
		}
	}		
}
