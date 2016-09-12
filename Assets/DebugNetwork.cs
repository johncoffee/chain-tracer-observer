using UnityEngine;
using System.Collections;

public class DebugNetwork : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("my IP " + Network.player.ipAddress);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
