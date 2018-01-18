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
		public ParticleSystem embersParticleSystem;

		private Collider poiCollider;
		public string id = "-1";
		public Canvas poiUI;
		public Image poiImage;
		public Text title;
		public Text description;
		public Text unitiesText;

		private Mosaic mosaic;

		// Use this for initialization
		void Start () {
			poiCollider = GetComponent<Collider> ();		
		}

		// Update is called once per frame
		void Update () {
			SetupEnabled ();
		}

		public void Enable(Mosaic mosaic){
			if (mosaic.GetInitialSupply () > 0) {
				SetEnabled (true);
				SetMosaic (mosaic);
			}
		}

		private void SetMosaic(Mosaic mosaic){
			this.mosaic = mosaic;
			MosaicJsonDescription poiDescription = mosaic.GetJsonDescription ();
			StartCoroutine (ImageUtils.LoadImage (poiDescription.img_url,poiImage));
			title.text = poiDescription.name;
			description.text = poiDescription.description;
			SetupUnities (mosaic);
		}

		private void SetupUnities(Mosaic mosaic){
			int unities = mosaic.GetInitialSupply();
			unitiesText.text = unities + " available";//TODO: Put this string as parameter
		}

		public void OnPick(){
			Debug.Log ("OnPick");
			if (mosaic != null) {
				StartCoroutine(GameObject.FindGameObjectWithTag ("HunterApi").GetComponent<HunterApi> ().pick (mosaic));
			}
		}

		private void SetupEnabled(){
			if (poiEnabled) {
				Enable ();
			} else {
				Disable ();
			}
		}


		public void Enable(){
			if (!embersParticleSystem.isPlaying) {
				embersParticleSystem.Play ();
			}
		}

		public void Disable(){
			if (embersParticleSystem.isPlaying) {
				embersParticleSystem.Stop ();
			}
		}

		private void SetEnabled(bool poiEnabled){
			this.poiEnabled = poiEnabled;
			SetupEnabled ();
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