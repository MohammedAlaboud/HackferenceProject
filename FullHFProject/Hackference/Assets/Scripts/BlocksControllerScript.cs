using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksControllerScript : MonoBehaviour {

	public GameObject basic_block;
	public float block_scale;
	float ymax;

	// Use this for initialization
	void Start () {
		float WallSpeed = transform.parent.GetComponent<WallSpawnerScript>().WallSpeed;
		block_scale = 1;
		ymax = GameObject.Find("PlayerController").GetComponent<PlayerControllerScript>().The_ymax;

		float gapSize = Random.Range (0.35f, 0.9f) * ymax;
		float gapPos = Random.Range (- 0.95f + gapSize/2f, 0.9501f - gapSize/2f) * ymax;
	
		float LSize = gapPos + ymax;
		float USize = 2 * ymax - gapSize - LSize;
		float LPos = LSize/2f -  ymax;
		float UPos = - ymax + LSize + gapSize + USize/2f;

		GameObject LowerBlock = Instantiate (basic_block, new Vector3 (0f, 0f, 0f), Quaternion.identity, transform);
		LowerBlock.transform.localPosition = new Vector3 (0f, LPos, 0f);
		LowerBlock.transform.localScale = new Vector3 (1, LSize/2f, 1);
		GameObject UpperBlock = Instantiate (basic_block, new Vector3 (0f, 0f, 0f), Quaternion.identity, transform);
		UpperBlock.transform.localPosition = new Vector3 (0f, UPos, 0f);
		UpperBlock.transform.localScale = new Vector3 (1, USize/2f, 1);

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-WallSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < -20) {
			Destroy(gameObject);
		}

	}
}
