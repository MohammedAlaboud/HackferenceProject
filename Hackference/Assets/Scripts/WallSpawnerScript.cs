using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnerScript : MonoBehaviour {

	public float WallSpeed, WallRate, timeCounter;
	public GameObject WallPrefab;
	bool WallReady;

	// Use this for initialization
	void Start () {
		WallSpeed = 6;
		WallRate = 6;
		WallReady = false;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (WallReady) {
			Instantiate (WallPrefab, transform.position, Quaternion.identity, transform);
			timeCounter = 0;
			WallReady = false;
			WallSpeed += 0.5f;
			WallRate -= 0.35f;
		} else {
			timeCounter += Time.deltaTime;
			if (timeCounter >= WallRate) {
				WallReady = true;
			}
		}
	}
}
