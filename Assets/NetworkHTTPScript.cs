using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkHTTPScript : MonoBehaviour {

	public string URL = "";
	string jsonString = "";

	void Start () {
		Download();
	}

	void Download() {
		StartCoroutine(GetText(URL));
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			Download();

		if (jsonString.Length > 0) {
			ProcessJsonString(jsonString);
			jsonString = "";
		}
	}

	Record[] ProcessJsonString(string jsonString) {
		string[] entities = jsonString.Split('\n');
		Record[] records  = new Record[entities.Length];

		for (int i = 0; i < records.Length; i++) {
			records[i] = JsonUtility.FromJson<Record>(entities[i]);
		}

		Debug.Log(records[0].owner);

		return records;
	}

	IEnumerator GetText(string url) {
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();

		if (www.isError) {
			Debug.Log(www.error);
		}
		else {
			// Show results as text
			jsonString += www.downloadHandler.text;
			www.downloadHandler.Dispose();
		}
	}
}

public class Record {
	public string key;
	public string time;
	public string lat;
	public string lng;
	public string unregisterCost;
	public string owner;

	public override string ToString() {
		return "Record with key " + key.ToString();
	}
}