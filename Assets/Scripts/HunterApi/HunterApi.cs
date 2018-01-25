using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NemApi.Models;
using UniRx;
using System.Text;
using UnityEngine.Networking;
using System;

public class HunterApi : MonoBehaviour {

	public string host = "http://server.pautena.com";
	public int port = 3000;

	private string  baseUrl;


	void Awake(){
		baseUrl= host + ":" + port;
	}

	public IEnumerator Pick(Mosaic mosaic){
		string url = baseUrl + "/bounties/pick";

		//TODO: Pick user address
		string bodyJsonString = "name="+mosaic.id.name+"&address=TAUYBF-WNP3D2-6H3UEG-2ED6T6-DI6YMN-3EGEJ3-LKFE";
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

		yield return request.SendWebRequest();

		Debug.Log("Response: " + request.downloadHandler.text);
	}

	public IEnumerator Exchange(string message,Mosaic mosaic,string privateKey,Action<ExchangeResponse> callback){
		string url = baseUrl + "/bounties/exchange";

		Debug.Log ("url: " + url);

		//TODO: Pick user address
		string bodyJsonString = "name="+mosaic.id.name+"&privateKey="+privateKey+"&message="+message;
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

		yield return request.SendWebRequest();

		string response = request.downloadHandler.text;
		Debug.Log("Response: " + response);

		ExchangeResponse exchangeResponse = JsonUtility.FromJson<ExchangeResponse> (response);
		callback.Invoke (exchangeResponse);
	}
}
