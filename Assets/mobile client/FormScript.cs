using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FormScript : MonoBehaviour {

	public Toggle updateToggle = null;
	public InputField hostInput = null;

	public string fieldsTag;
	InputField[] fields;

	public string URL {
		get {
			return "http://" + ((hostInput != null && hostInput.text.Length > 0) ? hostInput.text : "localhost") + ":3000/record"; 	
		}
	}

	void Start () {
		Debug.Log("my IP " + Network.player.ipAddress);
		Bootstrap();
	}

	void Bootstrap () {
		fields = FormScript.BootstrapFields(fieldsTag);
		Debug.Log("found " + fields.Length + " x InputField");
	}

	public void Submit() {
		var record = FormScript.CollectValuesFromInputFields(fields);
		var serialized = JsonUtility.ToJson(record);

		var url = (updateToggle != null && updateToggle.isOn) ? URL + "/" + record.key : URL;
		StartCoroutine(FormScript.PutJSON(url, serialized));
	}




	// helpers

	public static InputField[] BootstrapFields(string fieldsTag) {
		GameObject[] tagged = GameObject.FindGameObjectsWithTag(fieldsTag);

		InputField[] fields = new InputField[tagged.Length];
		for (int i = 0; i < fields.Length; i++) {
			fields[i] = tagged[i].GetComponent<InputField>();
		}
		return fields;
	}


	public static Record CollectValuesFromInputFields(InputField[] textsFields) {
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



	public static IEnumerator PutJSON(string url, string data) {
		UnityWebRequest www = UnityWebRequest.Put(url, data);
		www.SetRequestHeader("Content-Type", "application/json");
		yield return www.Send();

		if (www.isError) {
			Debug.LogWarning(www.error);
		}
		else {
			// Show results as text
			Debug.Log("Done PUT. Response: " + www.downloadHandler.text);
		}
		www.downloadHandler.Dispose();		
	}

	// holy f--- it's complicated to POST

	//	static byte[] GetBytes(string str)
	//	{
	//		byte[] bytes = new byte[str.Length * sizeof(char)];
	//		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
	//		return bytes;
	//	}
	//
	//	IEnumerator PostJSON(string url, string data) {
	//		var www = new UnityWebRequest(url);
	//		www.uploadHandler = new UploadHandlerRaw(GetBytes(data));
	//		www.downloadHandler = new DownloadHandlerBuffer();
	//		www.method = UnityWebRequest.kHttpVerbPOST;
	//
	//		www.SetRequestHeader("Content-Type", "application/json");
	//
	//		Debug.Log("POST data", www.ToString());
	//
	//		yield return www.Send();
	//
	//		if (www.isError) {
	//			Debug.LogWarning(www.error);
	//		}
	//		else {
	//			// Show results as text
	//			Debug.Log("Done POST. Response: " + www.downloadHandler.text);
	//		}
	//		www.downloadHandler.Dispose();		
	//	}

}
	