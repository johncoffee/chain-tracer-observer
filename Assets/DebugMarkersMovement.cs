using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DebugMarkersMovement : MonoBehaviour {

	public MarkersManager mm = null;
	public float delay = 1f;
	public int maxMarkers = 30;

	void Start() {
		StartSpawning();

		var r = new Record();
		r.key = Random.value.ToString();
		r.lat = ( 55.66f).ToString();
		r.lng = ( 12.54f ).ToString();
		mm.Add(r);
	}

	public void Blarh ()
	{		
		if (Random.value > .5f && mm.markers.Count >= 1) {
//			Debug.Log("update");
			var m = mm.markers[(int)Mathf.Floor(Random.value * mm.markers.Count)];
			var r = m.Record;
			r.lat = ( -90f + (Random.value * 180f) ).ToString();
			r.lng = ( -180f + (Random.value * 180f * 2f) ).ToString();
			m.Record = r;
		}
		else if (mm.markers.Count < maxMarkers) {
//			Debug.Log("add");
			var r = new Record();
			r.key = Random.value.ToString();
			r.lat = ( -90f + (Random.value * 180f) ).ToString();
			r.lng = ( -180f + (Random.value * 180f * 2f) ).ToString();
			mm.Add(r);
		}
	}

	void Update (){
		if (Input.GetKeyDown(KeyCode.F6)) {
			Blarh();
		}
	}
		
	void StartSpawning() {
		StartCoroutine(WaitAndSpawn());
	}

	IEnumerator WaitAndSpawn() {				
		yield return new WaitForSeconds(delay);
		Blarh();
		StartSpawning();
//		FetchRecords();
//		if (isPolling) {
//			StartPolling();
//		}
	}
}

