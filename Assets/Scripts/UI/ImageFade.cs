using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFade: MonoBehaviour {

	// the image you want to fade, assign in inspector
	public Image img;
	public float duration = 2f;

	public void Awake(){
		StartCoroutine(FadeIn());
	}

	public IEnumerator FadeIn(){
		for (float i = duration; i >= 0; i -= Time.deltaTime){
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}
		img.enabled = false;
	}

	public IEnumerator FadeOut(){
		img.enabled = true;
		for (float i = 0; i <= duration; i += Time.deltaTime){
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}
	}
}
