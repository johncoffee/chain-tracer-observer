using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public GameObject loadingVisual;
	public GameObject hasLoadingInterface;

	ILoading loadingComponent;
	bool lastState = false;

	public void ShowLoading (bool enabled) {
		loadingVisual.SetActive(enabled);
	}

	void Start() {
		loadingComponent = hasLoadingInterface.GetComponent<ILoading>();
		lastState = loadingVisual.activeSelf;
	}

	void Update() {
		bool newState = (loadingComponent != null) ? loadingComponent.IsLoading : false;
		if (newState != lastState) {
			ShowLoading(newState);
			lastState = newState;
		}
	}
}
