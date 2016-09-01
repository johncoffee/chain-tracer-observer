using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public TextAppear textAppear;
	public Text textField;

	public string Body {
		set {
			textAppear.SetText(value);
		}
		get {
			return textField.text;
		}
	}
}
