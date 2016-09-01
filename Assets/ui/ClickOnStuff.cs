
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

			if (Physics.Raycast(ray, out hit, 100f)) {

				if (currentOpenCard) {
					Destroy (currentOpenCard);
				}

				GameObject canvas = GameObject.Find("Canvas");
				GameObject go = (GameObject) Instantiate (cardPrefab, canvas.transform, false);
				// go.transform.SetParent(canvas.transform, false);

				Card cardApi = go.GetComponent<Card>();
				cardApi.Body = "Derp herp\nblabla\n stuff.....";

				currentOpenCard = go;
			}
		}
	
	}
}
