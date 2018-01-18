using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUtils {

	public static IEnumerator LoadImage(string url,Image image){
		WWW www = new WWW(url);
		yield return www;
		image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

	}


}
