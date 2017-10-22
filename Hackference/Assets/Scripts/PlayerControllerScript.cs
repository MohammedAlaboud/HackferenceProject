using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public GameObject player, heart;
	public Sprite Red_sprite, Blue_sprite, Redsplosion, Bluesplosion, RedWins, BlueWins, Draw;
	public int Red_Lives, Blue_Lives;
	public float The_ymax;
	GameObject Blue_Player, Red_Player;
	player_script Red_Script, Blue_Script;
	Vector3 RedInitialHeartPosition, BlueInitialHeartPosition, dHeart;
	List<GameObject> RedHeartList, BlueHeartList;

	// Use this for initialization
	void Awake () {
		The_ymax = 3;
		Red_Lives = 4;
		Blue_Lives = 4;
	}


	void Start () {
		RedInitialHeartPosition = new Vector3 (8f,-0.7f,0f);
		BlueInitialHeartPosition = new Vector3 (8f,4.3f,0f);
		dHeart = new Vector3 (0.9f, 0f, 0f);

		Red_Player = InstantiateRedPlayer ();
		Blue_Player = InstantiateBluePlayer ();

		RedHeartList = new List<GameObject> (4);
		BlueHeartList = new List<GameObject> (4);
		for (int i = 0; i < 4; i++) {
			Vector3 Pos = RedInitialHeartPosition + (float)i * dHeart;
			RedHeartList.Add (Instantiate (heart, Pos, Quaternion.identity, transform));
		}

		for (int i = 0; i < 4; i++) {
			Vector3 Pos2 = BlueInitialHeartPosition + (float)i * dHeart;
			BlueHeartList.Add (Instantiate (heart, Pos2, Quaternion.identity, transform));
		}


	}
	
	// Update is called once per frame
	void Update () {

		DestructionCheck ();

		if (Red_Lives < RedHeartList.Count) {
			Destroy (RedHeartList [Red_Lives]);
		}

		if (Blue_Lives < BlueHeartList.Count) {
			Destroy (BlueHeartList [Blue_Lives]);
		}

		if (Red_Lives == 0 && Blue_Lives == 0) {
			GetComponent<SpriteRenderer>().sprite = Draw;
			Time.timeScale = 0;
		}

		else if (Red_Lives == 0) {
			GetComponent<SpriteRenderer>().sprite = BlueWins;
			Time.timeScale = 0;
		}

		else if (Blue_Lives == 0) {
			GetComponent<SpriteRenderer>().sprite = RedWins;
			Time.timeScale = 0;
		}


	}

	void DestructionCheck () {
		if (!Red_Script.Existing) {
			Red_Lives -= 1;
			InstantiateRedPlayer ();
		}

		if (!Blue_Script.Existing) {
			Blue_Lives -= 1;
			InstantiateBluePlayer ();
		}
	}

	GameObject InstantiateRedPlayer () {
		GameObject Red_Player = Instantiate (player,  new Vector3 (-9f, -3.2f, 0f), Quaternion.identity, transform);

		Red_Script = Red_Player.GetComponent<player_script> ();
		Red_Script.ymax = The_ymax;
		Red_Script.player_Sprite = Red_sprite;
		Red_Script.myAxis = "Vertical";
		Red_Script.splosion = Redsplosion;

		return Red_Player;
	}


	GameObject InstantiateBluePlayer () {
		GameObject Blue_Player = Instantiate (player,  new Vector3 (-9f, 3.2f, 0f), Quaternion.identity, transform);
	
		Blue_Script = Blue_Player.GetComponent<player_script> ();
		Blue_Script.ymax = The_ymax;
		Blue_Script.player_Sprite = Blue_sprite;
		Blue_Script.myAxis = "Vertical2";
		Blue_Script.splosion = Bluesplosion;

		return Blue_Player;
	}
}
