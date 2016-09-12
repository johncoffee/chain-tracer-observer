
using UnityEngine;
using System.Collections;

public class ClickOnStuff : MonoBehaviour {

	public GameObject cardPrefab;
	public GameObject currentOpenCard = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 10000f)) {
				var go = hit.collider.gameObject;
				if (go.tag == "stick") {
					var record = go.GetComponentInParent<Marker>().Record;
					var str = string.Format("Lat {0}\n Lng {1}\n{2}", record.lat, record.lng, record.time);
					OpenCard(str);
				}
			}
		}	
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
