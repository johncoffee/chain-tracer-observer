using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
		public float power = 120f;
	public bool shakeOnSpace = false;
	public bool shakeOnStart = false;

	void Start() {
		if (shakeOnStart) 
			Shake();
	}

		// Use this for initialization
		public void Shake ()
		{
				Vector3 amount = new Vector3 (Random.Range (-power / 2f, power / 2f), Random.Range (-power / 2f, power / 2f), 0f);		
				iTween.ShakePosition (Camera.main.gameObject, amount, 0.2f);
		}

	void Update() {
		if (shakeOnSpace && Input.GetKeyDown(KeyCode.Space)) {
			Shake();
		}
	}
}
