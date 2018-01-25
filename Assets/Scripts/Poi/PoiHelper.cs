using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mapbox.Unity.MeshGeneration.Interfaces;
using NemApi.Models;
using UnityEngine.UI;
using Models;
using Models.Managers;


namespace Poi{
	public class PoiHelper : MonoBehaviour,IFeaturePropertySettable {

		public ParticleSystem embersParticleSystem;

		private Collider poiCollider;
		public string id = "-1";
		public Image poiImage;
		public Text title;
		public Text description;
		public Animator animator;
		public Button pickButton;
		public Animator accomplishedAnimator;

		private Mosaic mosaic;
		private int quantity;
		private User user;

		// Use this for initialization
		void Start () {
			poiCollider = GetComponent<Collider> ();
			quantity = 0;
			user = UserManager.GetInstance ().GetUser ();
		}

		private void SetupEnable(){
			Debug.Log ("IsEnabled: " + IsEnabled());
			if (IsEnabled ()) {
				Enable ();
			} else {
				Disable ();
			}
		}

		public void SetMosaic(Mosaic mosaic,byte network){
			this.mosaic = mosaic;
			MosaicJsonDescription poiDescription = mosaic.GetJsonDescription ();
			StartCoroutine (ImageUtils.LoadImage (poiDescription.img_url,poiImage));
			title.text = poiDescription.name;
			description.text = poiDescription.description;
			SetupEnable ();
			CheckHasMosaic (mosaic, network);
		}

		private void CheckHasMosaic(Mosaic mosaic,byte network){
			string address = user.GetAddress (network);
			Debug.Log ("address:" + address);
		}

		public void SetQuantity(int quantity){
			this.quantity = quantity;
			SetupEnable ();
		}

		public void OnPick(){
			Debug.Log ("OnPick");
			if (IsEnabled()) {
				StartCoroutine(GameObject.FindGameObjectWithTag ("HunterApi").GetComponent<HunterApi> ().Pick (mosaic));
				accomplishedAnimator.SetTrigger ("Show");
				pickButton.gameObject.SetActive (false);
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

		public bool IsEnabled(){
			return mosaic != null && quantity > 0;
		}

		public void OnClick(){
			if (/*PlayerInsideCollider () &&*/ IsEnabled()) {
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
			animator.SetTrigger ("Show");
		}

		public void HidePoiUI(){
			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<MainUIManager> ().Show ();
			animator.SetTrigger ("Hide");
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

		public Mosaic GetMosaic(){
			return mosaic;
		}

	}

}