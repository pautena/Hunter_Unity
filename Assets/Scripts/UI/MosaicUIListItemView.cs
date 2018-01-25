using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using UnityEngine.UI;
using NemApi;

public class MosaicUIListItemView : MonoBehaviour {

	public RectTransform rect;
	public Image icon;
	public Text name;
	public Button exchangeBtn;
	public float marginBottom=10f;

	private Mosaic mosaic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize(OwnedMosaic ownedMosaic,Mosaic mosaic,int index){
		Debug.Log ("Initialize("+index+") -> ownedMosaic: " + ownedMosaic+", mosaic: "+mosaic);
		SetupHeight (index);
		SetupMosaicInfo (mosaic);
		SetupExchange (ownedMosaic);
	}

	public void SetupHeight(int index){
		float height = rect.rect.height;
		float posY = -((height+marginBottom) *index);
		Debug.Log ("posY: " + posY);
		transform.localPosition = new Vector3 (transform.localPosition.x, posY, transform.localPosition.z);
	}

	private void SetupMosaicInfo(Mosaic mosaic){
		this.mosaic = mosaic;
		MosaicJsonDescription description = mosaic.GetJsonDescription ();
		name.text = description.name;
		StartCoroutine(ImageUtils.LoadImage(description.img_url,icon));
	}

	private void SetupExchange(OwnedMosaic ownedMosaic){
		//TODO: Can exchange anything?
	}

	public void onRequestExchange(){
		GameObject.FindGameObjectWithTag ("UIExchange").GetComponent<UIExchangeManager> ().ShowExchangeDialog (mosaic);
	}

	public Mosaic GetMosaic(){
		return mosaic;
	}
}
