﻿using UnityEngine;
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
			records.Add(Record.Clone(value));
			UpdateViewModel(value);
		}
		get {
			return records[records.Count-1];
		}
	}

	void UpdateViewModel(Record record) {		
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

	public static bool DiffLocation(Record r1, Record r2) {
		return (r1.lat != r2.lat || r1.lng != r2.lng);
	}
 }


public class Record {
	public string key = "-1";
	public string time;
	public string lat = "";
	public string lng = "";
	public string owner;

	public static Record Clone(Record r) {
		var r2 = new Record();
		r2.key = r.key;
		r2.lat = r.lat;
		r2.lng = r.lng;
		r2.time = r.time;
		r2.owner = r.owner;
		return r2;
	}

	public override string ToString() {
		return "Key " + key.ToString() + ". [" + lat +", " + lng + "]";
	}
}