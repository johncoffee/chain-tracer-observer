using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkersManager : MonoBehaviour {

	public string url = "http://localhost:3000/records?format=sjon";
	public GameObject markerPrefab;
	public List<Marker> markers = new List<Marker>(); 

	bool isPolling = false;
	public bool IsPolling {
		get {return isPolling;}
		set {			
			if (value && !isPolling) {
				StartPolling();
			}
			isPolling = value;
		}
	}

	void Start () {
//		Polling = true;
	}

	void FetchRecords() {
		var http = GetComponent<NetworkHTTPScript>();
		http.FetchRecords(url, "OnRecords");
	}

	public void OnRecords(Record[] updatedRecords) {
		MergeRecords(updatedRecords);
	}

	public void MergeRecords(Record[] records) {
		// update existing

		for (int j = 0; j < records.Length; j++) {													
			Record newRecord = records[j];

			Marker marker = GetMarkerByKey(newRecord, markers);
			if (marker == null) {
				Debug.Log("Added " + newRecord);
				Add(newRecord);	
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
		StartCoroutine(WaitAndFetch());
	}

	IEnumerator WaitAndFetch() {		
		yield return new WaitForSeconds(5);
		FetchRecords();
		if (isPolling) {
			StartPolling();
		}
	}

	public static Marker GetMarkerByKey(Record record, List<Marker> markers) {
		for (int i = 0; i < markers.Count; i++) {			
			Marker marker = markers[i];
			if (record.key == marker.Record.key) {
				return marker;
			}
		}

		return null;
	}

	public void Add(Record record) {
		GameObject go = (GameObject)Instantiate(markerPrefab);
		var marker = go.GetComponent<Marker>();
		marker.Record = record;
		markers.Add(marker);	
		SendMessage("MarkerAdded", marker);
	}
		
}