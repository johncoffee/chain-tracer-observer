
using UnityEngine;
using System.Collections;

public class ClickOnStuff : MonoBehaviour {

	public GameObject cardPrefab;
	public GameObject currentOpenCard = null;
	public MarkersManager mm;

	// Use this for initialization
	void Start () {
		mm = GetComponent<MarkersManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 10000f)) {
				var go = hit.collider.gameObject;
				if (go.tag == "stick") {
					var marker = go.GetComponentInParent<Marker>();
					var record = marker.Record;
					var str = string.Format("Lat {0}\nLng {1}\n{2}", record.lat, record.lng, record.time);

					OpenCard(str);

					CreateTrail(marker);
				}
			}
		}	
	}

	public void CreateTrail (Marker marker) {
		mm.Clear();
		mm.Add(marker.Records.ToArray());
	}

	public void OpenCard (string body) {
		if (currentOpenCard != null) {
			Destroy (currentOpenCard);
		}

		GameObject canvas = GameObject.Find("Canvas");
		GameObject go = (GameObject) Instantiate (cardPrefab, canvas.transform, false);

		Card cardApi = go.GetComponent<Card>();
		cardApi.Body = body;

		currentOpenCard = go;
	}
}
