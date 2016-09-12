using UnityEngine;
using System.Collections;

public class MarkerEffects : MonoBehaviour {

	public void MarkerAdded(Marker marker) {
		Camera.main.GetComponent<CameraShake>().Shake();
	}

	public void Updated(Marker marker) {
		
	}

	void RecordMoved() {
		
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
