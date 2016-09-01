using UnityEngine;
using System.Collections;

public class Spawner2 : MonoBehaviour {

	public GameObject prefab;
	public float height = 10f; 
	public int boxCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (boxCount < 3) {
			Spawn();
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			Spawn ();
		}
		
	}

	public void Spawn () {
		var go = (GameObject)Instantiate(prefab);
		boxCount++;
		go.transform.position = go.transform.position + new Vector3(0f, height + boxCount, 0f);
	}
}
