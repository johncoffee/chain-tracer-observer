using UnityEngine;
using System.Collections;

public class EarthObj : MonoBehaviour {
	public GameObject earth = null;
	public float earthRadius = 5f;

	void Start () {
		if (!earth) {
			earth = GameObject.FindGameObjectWithTag("earth");
			if (!earth) Debug.LogError("not found: 'earth'");
			else {
				this.transform.position = this.transform.up * earthRadius;
			}
		}
	}

	void Update () {
		this.transform.LookAt(earth.transform.position);		
	}
}
