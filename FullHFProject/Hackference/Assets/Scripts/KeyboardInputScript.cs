using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleJSON;


public class KeyboardInputScript : MonoBehaviour {

	float TextCheckInterval, timeCounter;
	bool CheckReady;
	public GameObject FlashBackground, pups, cats, troll;
	List<string> BlueStrings, RedStrings;
	SimpleJSON.JSONNode eventList;

	// Use this for initialization
	void Start () {
		TextCheckInterval = 4;
		CheckReady = false;
		timeCounter = 0;
		BlueStrings = new List<string> ();
		RedStrings = new List<string> ();
	}

	// Update is called once per frame
	void Update () {
		CheckForKeyPresses ();

		if (CheckReady) {
			List<string> MostPopularStrings = DoTextCheck ();

			timeCounter = 0;
			CheckReady = false;

			RunCommands (MostPopularStrings);

		} else {
			timeCounter += Time.deltaTime;
			if (timeCounter >= TextCheckInterval) {
				CheckReady = true;
			}
		}
	}

	void CheckForKeyPresses () {
		if (Input.GetKeyDown ("4")) {
			BlueStrings.Add ("SLOW");
		}
		else if (Input.GetKeyDown ("5")) {
			BlueStrings.Add ("FAST");
		}
		else if (Input.GetKeyDown ("6")) {
			BlueStrings.Add ("FLASH");
		}
		else if (Input.GetKeyDown ("7")) {
			BlueStrings.Add ("CATS");
		}
		else if (Input.GetKeyDown ("8")) {
			BlueStrings.Add ("DOGS");
		}
		else if (Input.GetKeyDown ("9")) {
			BlueStrings.Add ("TROLL");
		}
		else if (Input.GetKeyDown ("r")) {
			RedStrings.Add ("SLOW");
		}
		else if (Input.GetKeyDown ("t")) {
			RedStrings.Add ("FAST");
		}
		else if (Input.GetKeyDown ("y")) {
			RedStrings.Add ("FLASH");
		}
		else if (Input.GetKeyDown ("u")) {
			RedStrings.Add ("CATS");
		}
		else if (Input.GetKeyDown ("i")) {
			RedStrings.Add ("DOGS");
		}
		else if (Input.GetKeyDown ("o")) {
			RedStrings.Add ("TROLL");
		}
	}





	List<string> DoTextCheck () {

		string RedResponse = "";
		string BlueResponse = "";

		if (BlueStrings.Count >= 1) {
			BlueResponse = BlueStrings.GroupBy (i => i).OrderByDescending (grp => grp.Count ()).Select (grp => grp.Key).First ();
		} else {
			BlueResponse = "No response";
		}


		if (RedStrings.Count >= 1) {
			RedResponse = RedStrings.GroupBy (i => i).OrderByDescending (grp => grp.Count ()).Select (grp => grp.Key).First ();
		} else {
			RedResponse = "No response";
		}

		//RedResponse = "TROLL"; //Simulate audience response
		//BlueResponse = "SLOW";

		BlueStrings.Clear ();
		RedStrings.Clear ();

		List<string> MostPopularStrings = new List<string> (2);
		MostPopularStrings.Add (BlueResponse); //Blue Team Response
		MostPopularStrings.Add (RedResponse); //Red Team Response
		return MostPopularStrings;
	}


	void RunCommands (List<string> Keywords) {
		for (int i = 0; i < 2; i++) {
			switch (Keywords [i]) {
			case "FLASH":
				RunFlash (i);
				break;
			case "SLOW":
				RunSlow (i);
				break;
			case "FAST":
				RunFast (i);
				break;
			case "DOGS":
				RunPuppies (i);
				break;
			case "CATS":
				RunKittens (i);
				break;
			case "TROLL":
				RunTroll (i);
				break;
			default:
				Debug.Log ("No response");
				break;
			}

		}
	}

	void RunFlash (int Team) {
		if (Team == 0) {
			Instantiate(FlashBackground, new Vector3 (0f, -3.2f, -0.5f), Quaternion.identity, transform);
		}
		else {
			Instantiate(FlashBackground, new Vector3 (0f, 3.2f, -0.5f), Quaternion.identity, transform);
		}
	}

	void RunSlow (int Team) {
		if (Team == 0) {
			GameObject.Find("Wall_Spawner").GetComponent<WallSpawnerScript>().WallSpeed -= 0.5f;
		}
		else {
			GameObject.Find("Red_spawner").GetComponent<WallSpawnerScript>().WallSpeed -= 0.5f;
		}

		return;
	}

	void RunFast (int Team) {
		if (Team == 0) {
			GameObject.Find("Red_spawner").GetComponent<WallSpawnerScript>().WallSpeed += 0.5f;
		}
		else {
			GameObject.Find("Wall_Spawner").GetComponent<WallSpawnerScript>().WallSpeed += 0.5f;
		}

		return;
	}

	void RunPuppies (int Team) {
		if (Team == 0) {
			Instantiate (pups, new Vector3 (-10.38f, 3f, 1f), Quaternion.identity, transform);
			Instantiate (pups, new Vector3 (10.38f, 3f, 1f), Quaternion.identity, transform);
		}
		else {
			Instantiate (pups, new Vector3 (-10.38f, -3f, 1f), Quaternion.identity, transform);
			Instantiate (pups, new Vector3 (10.38f, -3f, 1f), Quaternion.identity, transform);
		}
		return;
	}

	void RunKittens (int Team) {
		if (Team == 0) {
			Instantiate (cats, new Vector3 (-10.38f, 3f, 1f), Quaternion.identity, transform);
			Instantiate (cats, new Vector3 (10.38f, 3f, 1f), Quaternion.identity, transform);
		}
		else {
			Instantiate (cats, new Vector3 (-10.38f, -3f, 1f), Quaternion.identity, transform);
			Instantiate (cats, new Vector3 (10.38f, -3f, 1f), Quaternion.identity, transform);
		}
		return;
	}

	void RunTroll (int Team) {
		if (Team == 0) {
			Instantiate (troll, new Vector3 (15f, 3.75f, -1f), Quaternion.identity, transform);
		} else {
			Instantiate (troll, new Vector3 (15f, -3.75f, -1f), Quaternion.identity, transform);
			return;
		}
	}
}
