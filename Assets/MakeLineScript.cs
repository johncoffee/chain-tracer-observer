using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeLineScript : MonoBehaviour {

	public List<Transform> waypoints;

	void Start () {
		Debug.Log(waypoints.Count);
		Make(waypoints);
	}	

	void Make(List<Transform> waypoints) {
		Vector3[] positions = new Vector3[waypoints.Count];

		for (int i = 0; i < waypoints.Count; i++) {
			positions[i] = waypoints[i].position;
		}

		var line = GetComponent<LineRenderer>();
		line.SetPositions(positions);
	}
}
