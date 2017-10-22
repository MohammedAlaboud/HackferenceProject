using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-25f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -30) {
			Destroy(gameObject);
		}
	}
}
