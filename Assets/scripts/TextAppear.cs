using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextAppear : MonoBehaviour {

	public float wait = 0.35f;
	public float stepTime = 0.066f;

	// Use this for initialization
	void Start () {		
		var text = "Tx\n2016-07-05 21:17:58\nUnconfirmed";
		GetComponent<Text> ().text = "";
		StartCoroutine (waitToStart(text));
	}

	void StartAnimate(string text) {
		if (text.Length > 0) {
			StartCoroutine (Animate (text));		
		}
		else {
			Fabric.EventManager.Instance.PostEvent("letter",  Fabric.EventAction.StopSound);
		}
	}

	IEnumerator waitToStart(string text) {
		yield return new WaitForSeconds(wait);
		Fabric.EventManager.Instance.PostEvent("letter",  Fabric.EventAction.PlaySound);

		StartAnimate (text);
	}

	IEnumerator Animate (string text) {
		GetComponent<Text> ().text += text [0];
		text = text.Substring (1);

		yield return new WaitForSeconds(stepTime);

		StartAnimate (text);	
	}
}
