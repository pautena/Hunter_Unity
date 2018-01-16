using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mapbox.Unity.MeshGeneration.Interfaces;
using NemApi.Models;
using UnityEngine.UI;

namespace Poi{
	public class PoiHelper : MonoBehaviour,IFeaturePropertySettable {

		public bool poiEnabled=true;
		public GameObject enabledGameObject;

		private Collider poiCollider;
		public string id = "-1";
		public Canvas poiUI;
		public Image poiImage;
		public Text title;
		public Text description;

		// Use this for initialization
		void Start () {
			poiCollider = GetComponent<Collider> ();		
		}

		// Update is called once per frame
		void Update () {

			if (poiEnabled) {
				Enable ();
			} else {
				Disable ();
			}
		}

		public void Enable(Mosaic mosaic){
			//TODO: Check if can enable with this mosaic;
			SetEnabled(true);
			SetupMosaic (mosaic);
		}

		private void SetupMosaic(Mosaic mosaic){

			PoiDescription poiDescription = JsonUtility.FromJson<PoiDescription> (mosaic.description);
			StartCoroutine (LoadImage (poiDescription.img_url));
			title.text = poiDescription.name;
			description.text = poiDescription.description;
		}

		IEnumerator LoadImage(string url)
		{
			WWW www = new WWW(url);
			yield return www;
			poiImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
		}


		public void Enable(){
			enabledGameObject.SetActive (true);
		}

		public void Disable(){
			enabledGameObject.SetActive (false);
		}

		private void SetEnabled(bool poiEnabled){
			this.poiEnabled = poiEnabled;
			enabledGameObject.SetActive (poiEnabled);
		}

		public void OnClick(){
			if (/*PlayerInsideCollider () &&*/ poiEnabled) {
				ShowPrize ();
			}
		}

		private void ShowPrize(){
			CameraToTarget cameraToTarget = Camera.main.GetComponent<CameraToTarget> ();
			ShowPoiUI ();
			cameraToTarget.SetTarget (transform);
		}

		public void HidePrize(){
			CameraToTarget cameraToTarget = Camera.main.GetComponent<CameraToTarget> ();
			cameraToTarget.RemoveTarget ();
			HidePoiUI ();
		}

		public void ShowPoiUI(){
			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<MainUIManager> ().Hide ();
			poiUI.enabled = true;
		}

		public void HidePoiUI(){
			poiUI.enabled = false;
			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<MainUIManager> ().Show ();
		}

		private bool PlayerInsideCollider(){
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			return player != null && poiCollider.bounds.Contains (player.transform.position);
		}

		public void Set(Dictionary<string, object> props){
			string id =  props ["id"].ToString();

			this.SetId (id);

		}

		private void SetId(string id){
			this.id = id;
			new PoiManager ().SetPoiName (gameObject,id);
		}

	}

}