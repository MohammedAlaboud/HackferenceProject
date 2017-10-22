using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour {

	float Duration, timeCounter;
	bool Ready;
	public Sprite FlashBackground;

	// Use this for initialization
	void Start () {
		Duration = 0.6f;
		Ready = false;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Ready) {
			Destroy (gameObject);
		} else {
			timeCounter += Time.deltaTime;
			if (timeCounter >= Duration ) {
				Ready = true;
			}
		}
	}
}
