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
		fields = FormsManager.BootstrapFields(fieldsTag);
		Debug.Log("found " + fields.Length + " x InputField");
	}

	public void Submit() {
		var record = FormsManager.CollectValuesFromInputFields(fields);
		var serialized = JsonUtility.ToJson(record);

		if (updateToggle != null && updateToggle.isOn) {
			var url = URL + "/" + record.key;
			StartCoroutine(FormsManager.PutJSON(url, serialized));
		}
		else {
			StartCoroutine(FormsManager.PutJSON(URL, serialized));
		}
	}

}
	