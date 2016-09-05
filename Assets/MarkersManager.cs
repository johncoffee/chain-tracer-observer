using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkersManager : MonoBehaviour {

	public string url = "http://localhost:3000/records?format=sjon";
	public GameObject markerPrefab;
	public List<Marker> markers = new List<Marker>(); 
	public bool polling = false;
	public bool Polling {
		get {return polling;}
		set {			
			if (value && !polling) {
				StartPolling();
			}
			polling = value;
		}
	}

	void Start () {
		Polling = true;
	}

	void FetchRecords() {
		var http = GetComponent<NetworkHTTPScript>();
		http.FetchRecords(url, "OnRecords");
	}

	public void OnRecords(Record[] updatedRecords) {
		// update existing

		for (int j = 0; j < updatedRecords.Length; j++) {													
			Record newRecord = updatedRecords[j];

			Marker marker = GetMarkerByKey(newRecord.key, markers);
			if (marker == null) {
				Debug.Log("Added " + newRecord);
				AddFromRecord(newRecord);	
			}
			else {
				Debug.Log("Updated " + newRecord);
				marker.Record = newRecord;
			}
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.F5)) {
			Debug.Log("updating...");
			FetchRecords();
		}
	}

	void StartPolling() {
		StartCoroutine(Poll());
	}

	IEnumerator Poll() {		
		yield return new WaitForSeconds(5);
		FetchRecords();
		if (polling) {
			StartPolling();
		}
	}

	public static Marker GetMarkerByKey(string key, List<Marker> markers) {
		for (int i = 0; i < markers.Count; i++) {			
			Marker marker = markers[i];
			if (key == marker.Record.key) {
				return marker;
			}
		}

		return null;
	}

	public void AddFromRecord(Record record) {
		GameObject go = (GameObject)Instantiate(markerPrefab);
		var marker = go.GetComponent<Marker>();
		marker.Record = record;
		markers.Add(marker);	
	}

}