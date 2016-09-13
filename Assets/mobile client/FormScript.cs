using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;



public class FormScript : BaseForm {

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
		StartCoroutine(PutJSON(url, serialized));
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
			var fieldInfo = recordType.GetField(name);
			if (fieldInfo != null) {
				var text = textsFields[i].text;				
				fieldInfo.SetValue(record, text);
			}
			else {
				Debug.LogWarning("Did not set member " + name);
			}
		}

		return record;
	}

}
	