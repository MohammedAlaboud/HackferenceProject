using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnerScript : MonoBehaviour {

	public float WallSpeed, WallRate, timeCounter;
	public GameObject WallPrefab;
	bool WallReady, isPause;

	// Use this for initialization
	void Start () {
		WallSpeed = 4;
		WallRate = 8;
		WallReady = false;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			isPause = !isPause;
		}

			if (WallReady) {
				Instantiate (WallPrefab, transform.position, Quaternion.identity, transform);
				timeCounter = 0;
				WallReady = false;
				WallSpeed += 0.45f;
				WallRate -= 0.3f;
			} else {
				timeCounter += Time.deltaTime;
				if (timeCounter >= WallRate) {
					if (!isPause) {
						WallReady = true;
					}
				}
			}
	}
}
