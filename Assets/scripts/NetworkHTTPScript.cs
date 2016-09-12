using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkHTTPScript : MonoBehaviour {

	string jsonString = "";
	string msgToCall = "";

	public void FetchRecords(string URL, string callbackName) {
		msgToCall = callbackName;
		StartCoroutine(GetText(URL));
	}

	void Update() {		
		if (jsonString.Length > 0) {
			var records = ProcessJsonString(jsonString);
			SendMessage(msgToCall, records);
			jsonString = "";
			msgToCall = "";
		}
	}

	Record[] ProcessJsonString(string jsonString) {
		string[] entities = jsonString.Split('\n');
		Record[] records  = new Record[entities.Length];

		for (int i = 0; i < records.Length; i++) {
			records[i] = JsonUtility.FromJson<Record>(entities[i]);
		}

		Debug.Assert(records.Length > 0 && records[0].owner.Length > 0, " key should be string");

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
	public string owner;

	public static Record Clone(Record r) {
		var r2 = new Record();
		r2.key = r.key;
		r2.lat = r.lat;
		r2.lng = r.lng;
		r2.time = r.time;
		r2.owner = r.owner;
		return r2;
	}

	public override string ToString() {
		return "Record with key " + key.ToString();
	}
}