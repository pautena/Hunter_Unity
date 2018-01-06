using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFade: MonoBehaviour {

	// the image you want to fade, assign in inspector
	public Image img;
	public float fadeDuration = 2000f;

	public void Awake(){
		StartCoroutine(FadeIn());
	}

	public IEnumerator FadeIn(){
		return FadeImage (true);
	}

	public IEnumerator FadeOut(){
		return FadeImage (false);
	}

	private IEnumerator FadeImage(bool fadeAway){
		// fade from opaque to transparent
		if (fadeAway){
			// loop over 1 second backwards
			for (float i = fadeDuration; i >= 0; i -= Time.deltaTime){
				// set color with i as alpha
				img.color = new Color(1, 1, 1, i);
				yield return null;
			}
		}
		// fade from transparent to opaque
		else{
			// loop over 1 second
			for (float i = 0; i <= fadeDuration; i += Time.deltaTime){
				// set color with i as alpha
				img.color = new Color(1, 1, 1, i);
				yield return null;
			}
		}
	}
}
