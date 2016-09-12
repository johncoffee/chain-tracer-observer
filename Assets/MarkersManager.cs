using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkersManager : MonoBehaviour {

	public string url = "http://localhost:3000/records?format=sjon";
	public GameObject markerPrefab;
	public List<Marker> markers = new List<Marker>(); 
	public KeyCode updateKey;
	public float pollDelay = 3.33f;

	public bool startPolling = false;

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
		if (startPolling) {
			Debug.Log("updating...");
			FetchRecords();
			IsPolling = true;
		}
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
				Debug.Log("Updated ");
				if (Marker.DiffLocation(newRecord, marker.Record)) {
					Debug.Log(marker.Record);
					Debug.Log(newRecord);
					marker.Record = newRecord;
					SendMessage(MarkerEffects.Events.Moved);
				}
			}
		}
	}

	void Update() {		
		if (Input.GetKeyDown(updateKey)) {
			Debug.Log("updating...");
			FetchRecords();
		}
	}

	void StartPolling() {
		StartCoroutine(WaitAndFetch());
	}

	IEnumerator WaitAndFetch() {		
		yield return new WaitForSeconds(pollDelay);
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
		SendMessage(MarkerEffects.Events.Added);
	}

	public void Add (Record[] records) {
		for (int i = 0; i < records.Length; i++) {
			Add(records[i]);	
		}	
	}

	public void RemoveAt(int index) {
		Destroy(markers[index].gameObject);
		markers.RemoveAt(index);
		SendMessage(MarkerEffects.Events.Removed);
	}

	public void Clear() {
		for (int i = markers.Count-1; i >= 0; i--) {			
			Marker marker = markers[i];
			Destroy(marker.gameObject);
		}
		markers.Clear();

		SendMessage(MarkerEffects.Events.Removed);
	}
}