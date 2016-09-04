using UnityEngine;
using System.Reflection;

public class Marker : MonoBehaviour {
	
	Record record = null;

	public Transform eulerArrow;
		
	public Record Record {
		set {
			UpdateViewModel(value);
		}
		get {
			return record;
		}
	}

	void UpdateViewModel(Record record) {
		if (this.record != null && (record.lat != this.record.lat || record.lng != this.record.lng)) {
			SendMessage("RecordMoved");
		}
		UpdateEulerArrow(record.lat, record.lng);

		this.record = record;
	}

	void UpdateEulerArrow (float lat, float lng) {
		// Fix North is negative
		eulerArrow.rotation = Quaternion.Euler(lat, lng, 0f);
	}

	void UpdateEulerArrow (string lat, string lng) {
		float latFloat = 0f, lngFloat = 0f;
		if (float.TryParse(lat, out latFloat) && float.TryParse(lng, out lngFloat) ) {
			UpdateEulerArrow(latFloat, lngFloat);
		}
		else {
			Debug.LogWarning("Failed parsing [" + lat + " , " + lng + "]");
		}
	}
}
