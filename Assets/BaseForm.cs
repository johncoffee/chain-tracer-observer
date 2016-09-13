using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BaseForm : MonoBehaviour, ILoading {

	// implement ILoading
	bool loading = false;
	public bool IsLoading {
		get {
			return loading;
		}
	}

	public IEnumerator PutJSON(string url, string data) {
		UnityWebRequest www = UnityWebRequest.Put(url, data);
		www.SetRequestHeader("Content-Type", "application/json");
		loading = true;
		yield return www.Send();
		loading = false;
		if (www.isError) {
			Debug.LogWarning(www.error);
		}
		else {
			// Show results as text
			Debug.Log("Done PUT. Response: " + www.downloadHandler.text);
		}
		www.downloadHandler.Dispose();		
	}
}
