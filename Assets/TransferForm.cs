using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class TransferForm : BaseForm {

	public InputField hostInput = null;
	public string path = "/transfer";
	public InputField keyField;
	public InputField toField;
	public InputField fromField;

	class TransferRequest {
		public string key;
		public string from;
		public string to;
	}

	public string URL {
		get {
			return "http://" 
				+ hostInput.text  
				+ ":3000" + path; 	
		}
	}

	void Start () {
		Debug.Log("my IP " + Network.player.ipAddress);
	}

	public void Submit() {
		var vo = new TransferRequest();
		vo.key = keyField.text;
		vo.to = toField.text;
		vo.from = fromField.text;
		var serialized = JsonUtility.ToJson(vo);
		Debug.Log(URL);
		Debug.Log(serialized);
		StartCoroutine(PutJSON(URL, serialized));
	}
}
