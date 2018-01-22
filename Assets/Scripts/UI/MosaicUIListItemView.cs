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

	private void SetupHeight(int index){
		float height = rect.rect.height;
		float posY = -((height * index) + marginBottom);
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
		Debug.Log ("RequestExchange");
		string privateKey = "59e309f7db4cc17fcb1d0ddd85f331bf18be179cbb656eed8e2d69b98b5fb6ba";

		HunterApi connectionApi = GameObject.FindGameObjectWithTag ("HunterApi").GetComponent<HunterApi> ();

		StartCoroutine(connectionApi.Exchange(mosaic,privateKey));
		//TODO: Call to new api
	}
}
