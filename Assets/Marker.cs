using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

public class Marker : MonoBehaviour {
	
	List<Record> records = new List<Record>();

	public List<Record> Records {
		get {
			return records;
		}
	}

	public Transform eulerArrow;
		
	public Record Record {
		set {
			records.Add(value);
			UpdateViewModel(value);
		}
		get {
			return records[records.Count-1];
		}
	}

	void UpdateViewModel(Record record) {
		if (this.records.Count > 0 && (record.lat != this.Record.lat || record.lng != this.Record.lng)) {
			SendMessage("RecordMoved");
		}
		UpdateEulerArrow(record.lat, record.lng);
	}

	void UpdateEulerArrow (float lat, float lng) {
		eulerArrow.rotation = Quaternion.Euler(lat, lng, 0f);
	}

	void UpdateEulerArrow (string lat, string lng) {
		float latFloat = 0f, lngFloat = 0f;
		if (float.TryParse(lat, out latFloat) && float.TryParse(lng, out lngFloat) ) {
			UpdateEulerArrow(latFloat * -1f, lngFloat * -1f);
		}
		else {
			Debug.LogWarning("Failed parsing [" + lat + " , " + lng + "]");
		}
	}
}
