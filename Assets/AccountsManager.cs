using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccountsManager : MonoBehaviour {

	public GameObject buttonPrefab;
	public Text myAddressLabel;

	public string MyAddress {
		get {
			return myAddressLabel.text;
		}
		set {
			SetAccount(value);
		}
	}

	public void SetAccount(string address) {
		myAddressLabel.text = address;
	}
}
