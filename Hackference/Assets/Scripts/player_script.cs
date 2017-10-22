using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour {

	public float speed;
	public float ymax;
	float Ballymax, Ballymin;
	public string myAxis;
	public Sprite player_Sprite, splosion;
	public bool Existing;
	bool Dead, WaitOver;
	float timeCounter;
	Vector3 FrozenPosition;

	// Use this for initialization
	void Awake () {
		speed = 15;
	}

	void Start () {
		Dead = false;
		WaitOver = false;
		timeCounter = 0;
		Existing = true;

		float HalfBallWidth = 0.5f * GetComponent<Renderer> ().bounds.size.y;
		float InitialyPos = transform.position.y;

		Ballymax = InitialyPos + ymax - HalfBallWidth;
		Ballymin = InitialyPos - ymax + HalfBallWidth;

		GetComponent<SpriteRenderer> ().sprite = player_Sprite;

	}
	
	// Update is called once per frame
	void Update () {
		float v1 = Input.GetAxisRaw(myAxis);

		//float xPos = -9;
		float xPos = transform.position.x;
		float yPos = transform.position.y;

		//transform.position = new Vector3 (xPos, yPos, 0);

		if (yPos < Ballymax && yPos > Ballymin) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, v1 * speed);
			} else if ( yPos >= Ballymax) {
				if (v1 < 0) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, v1 * speed);
				} else {
					transform.position = new Vector3 (xPos, Ballymax, 0);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				}
			}
			else if (yPos <= Ballymin) {
				if (v1 > 0) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, v1 * speed);
				} else {
					transform.position = new Vector3 (xPos, Ballymin, 0);
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				}
			}

		if (Dead) {
			OnDead ();
		}

		}

	void OnCollisionEnter2D (Collision2D col) {
		Dead = true;
		FrozenPosition = transform.position;

	}

	void OnDead (){
		transform.position = FrozenPosition;
		GetComponent<SpriteRenderer> ().sprite = splosion;
		if (WaitOver) {
			Existing = false;
			Destroy (gameObject);
		} else {
			timeCounter += Time.deltaTime;
			if (timeCounter >= 1) {
				WaitOver = true;
			}
		}
	}


	}

