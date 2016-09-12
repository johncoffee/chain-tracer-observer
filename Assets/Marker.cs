using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public class Marker : MonoBehaviour {
	
	List<Record> record = new List<Record>();

	public Transform eulerArrow;
		
	public Record Record {
		set {
			UpdateViewModel(value);
		}
		get {
			return record[record.Count-1];
		}
	}

	void UpdateViewModel(Record record) {
		if (this.record.Count > 0 && (record.lat != this.Record.lat || record.lng != this.Record.lng)) {
			SendMessage("RecordMoved");
		}
		UpdateEulerArrow(record.lat, record.lng);
		this.record.Add(record);
	}

	void UpdateEulerArrow (float lat, float lng) {
		// Fix North is negative
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
