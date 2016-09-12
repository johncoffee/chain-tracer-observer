using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DebugMarkersMovement : MonoBehaviour {

	public MarkersManager mm = null;
	public float delay = 1f;
	public int maxMarkers = 30;

	void Start() {
		StartSpawning();
	}

	public void Blarh ()
	{		
		if (Random.value < .12f && mm.markers.Count >= 1) {
//			Debug.Log("update");
			var m = mm.markers[(int)Mathf.Floor(Random.value * mm.markers.Count)];
			var r = Record.Clone(m.Record);
			r.lat = ( -90f + (Random.value * 180f) ).ToString();
			r.lng = ( -180f + (Random.value * 180f * 2f) ).ToString();
			m.Record = r;
			MarkerEffects.Moved();
		}
		else if (Random.value < 0.02f && mm.markers.Count > 1) {
			mm.RemoveAt((int)Mathf.Floor( Random.value * mm.markers.Count));
			MarkerEffects.Removed();
		}
		else if (Random.value < 0.10f && mm.markers.Count < maxMarkers) {
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

