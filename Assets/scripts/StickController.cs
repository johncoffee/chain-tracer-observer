using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour {

	public Transform pointOnEarth;
	public float height = 5f;

	public GameObject box;

	// Use this for initialization
	void Start () {
		// resize stick
		box.transform.localScale = new Vector3(box.transform.localScale.x, box.transform.localScale.y, height);
		box.transform.position = new Vector3(0f, 0f, height/2f);

		// reposition
		transform.position = pointOnEarth.position;
		Transform earthPosition = GameObject.FindGameObjectWithTag("earth").transform;
		transform.LookAt(earthPosition);
		transform.localScale = new Vector3(1f, 1f, -1f);
	}

}
