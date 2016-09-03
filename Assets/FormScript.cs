using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FormScript : MonoBehaviour {

	public string URL = "";

	InputField[] fields;

	void Start () {
		fields = GameObject.FindObjectsOfType<InputField>();
		Debug.Log("found " + fields.Length + " x InputField");
	}

	void Update() {
//		if (Input.GetKeyDown(KeyCode.A)) {
//			var s = CollectValuesFromInputFields(fields);
//			Debug.Log(s);		
//		}
	}

	public void Submit() {
		var r = CollectValuesFromInputFields(fields);
		var serialized = JsonUtility.ToJson(r);
		StartCoroutine(PutJSON(URL, serialized));
	}

	Record CollectValuesFromInputFields(InputField[] textsFields) {
		var record = new Record();
		var recordType = record.GetType();
		for (int i = 0; i < textsFields.Length; i++) {
			string name = textsFields[i].gameObject.name;
			var prop = recordType.GetField(name);
			if (prop != null) {
				var text = textsFields[i].text;				
				prop.SetValue(record, text);
			}
			else {
				Debug.LogWarning("Did not set member " + name);
			}
		}
	
		return record;
	}

	IEnumerator PutJSON(string url, string data) {
		UnityWebRequest www = UnityWebRequest.Put(url, data);
		www.SetRequestHeader("Content-Type", "application/json");
		yield return www.Send();

		if (www.isError) {
			Debug.LogWarning(www.error);
		}
		else {
			// Show results as text
			Debug.Log("Done PUTing. Response: " + www.downloadHandler.text);
		}
		www.downloadHandler.Dispose();		
	}

}
