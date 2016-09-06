using UnityEngine;
using System.Collections;

public class IgnoreMe : MonoBehaviour {
	void OnAwake() {
		Destroy(this.gameObject);
	}
}