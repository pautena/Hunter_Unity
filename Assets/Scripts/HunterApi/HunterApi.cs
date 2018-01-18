using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using UniRx;
using System.Text;
using UnityEngine.Networking;

public class HunterApi : MonoBehaviour {

	public string host = "http://server.pautena.com";
	public int port = 3000;

	private string  baseUrl;


	void Awake(){
		baseUrl= host + ":" + port;
	}

	public IEnumerator pick(Mosaic mosaic){
		string url = baseUrl + "/bounties/pick";

		string bodyJsonString = "name=pixel&address=TAUYBF-WNP3D2-6H3UEG-2ED6T6-DI6YMN-3EGEJ3-LKFE";
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

		yield return request.SendWebRequest();

		Debug.Log("Response: " + request.downloadHandler.text);
	}
}
