using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkersManager : MonoBehaviour {

	public string url = "http://localhost:3000/records?format=sjon";
	public GameObject markerPrefab;
	public List<Marker> markers = new List<Marker>(); 

	void Start () {
//		UpdateMarkers();
	}

	public void OnRecords(Record[] records) {
		for (int i = 0; i < records.Length; i++) {
			AddFromRecord(records[i]);
		}	
	}

	public void AddFromRecord(Record record) {
		GameObject go = (GameObject)Instantiate(markerPrefab);
		var marker = go.GetComponent<Marker>();
		marker.Record = record;
		markers.Add(marker);	
	}

	public void UpdateMarkers() {
		// get records
		var http = GetComponent<NetworkHTTPScript>();
		http.FetchRecords(url, "OnRecords");

		// make Markers
			
	}

}
