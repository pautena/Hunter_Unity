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
		public Animator ownedAnimator;

		private Mosaic mosaic;
		private int quantity;
		private User user;
		private bool owned;

		// Use this for initialization
		void Start () {
			poiCollider = GetComponent<Collider> ();
			quantity = 0;
			owned = false;
			user = UserManager.GetInstance ().GetUser ();
		}

		private void SetupEnable(){
			if (IsOwned ()) {
				Owned ();
			}else if (IsEnabled ()) {
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
		}

		public void SetQuantity(int quantity){
			this.quantity = quantity;
			SetupEnable ();
		}

		public void SetOwned(bool owned){
			this.owned = owned;
			SetupEnable ();
		}

		public void OnPick(){
			if (IsEnabled() && !IsOwned()) {
				StartCoroutine(GameObject.FindGameObjectWithTag ("HunterApi").GetComponent<HunterApi> ().Pick (mosaic));
				accomplishedAnimator.SetTrigger ("Show");
				pickButton.gameObject.SetActive (false);
				owned = true;
				SetupEnable ();
			}
		}

		public void Owned(){
			if (embersParticleSystem.isPlaying) {
				embersParticleSystem.Stop ();
			}
			ownedAnimator.SetTrigger ("Show");
		}

		public void Enable(){
			if (!embersParticleSystem.isPlaying) {
				embersParticleSystem.Play ();
			}
			ownedAnimator.SetTrigger ("Hide");
		}

		public void Disable(){
			if (embersParticleSystem.isPlaying) {
				embersParticleSystem.Stop ();
			}
			ownedAnimator.SetTrigger ("Hide");
		}

		public bool IsEnabled(){
			return mosaic != null && quantity > 0;
		}

		public bool IsOwned(){
			return owned;
		}

		public void OnClick(){
			if (mosaic!=null) {
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
			EnablePickButton ();
		}

		private void EnablePickButton(){
			pickButton.gameObject.SetActive (PlayerInsideCollider ());
		}

		public void OnTriggerEnter(Collider other){

			Debug.Log ("OnTriggerEnter. other: " + other.name);

			if (other.tag == "Player" && IsEnabled()) {
				EnablePickButton ();
			}
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