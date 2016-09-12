using UnityEngine;
using System.Collections;

public class MarkerEffects : MonoBehaviour {

	public bool sfxOn = true;

	public static class Events {
		public static string Added = "MarkerAdded";
		public static string Moved = "MarkerMoved";
		public static string Removed = "MarkerRemoved";
	}

	public void MarkerAdded() {
//		Camera.main.GetComponent<CameraShake>().Shake();
		if (sfxOn)
		Fabric.EventManager.Instance.PostEvent("create",  Fabric.EventAction.PlaySound);
	}

	public void MarkerMoved() {
		if (sfxOn)
			Moved();
	}

	public static void Moved() {
		Fabric.EventManager.Instance.PostEvent("move",  Fabric.EventAction.PlaySound);
	}
		
	public void MarkerRemoved() {
		if (sfxOn)
			Removed();
	}


	public static void Removed() {
		Fabric.EventManager.Instance.PostEvent("destroy",  Fabric.EventAction.PlaySound);
	}
}
