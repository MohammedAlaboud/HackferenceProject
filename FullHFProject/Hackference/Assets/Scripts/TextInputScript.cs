using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleJSON;


public class TextInputScript : MonoBehaviour {

	float TextCheckInterval, timeCounter;
	bool CheckReady;
	public GameObject FlashBackground, pups, cats, troll;
	//public List<EventInfo> eventList;
	SimpleJSON.JSONNode eventList;

	// Use this for initialization
	void Start () {
		TextCheckInterval = 4;
		CheckReady = false;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckReady) {
			Debug.Log ("Doing Text Check");
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

	List<string> DoTextCheck () {
		//string text = System.IO.File.ReadAllText(@"C:\Users\Rosanna\Documents\GitHub\HackferenceProject\number.txt");
		string text = System.IO.File.ReadAllText(@"../number.txt");
		var N = JSON.Parse (text);

		eventList = N ["eventList"];
		//Debug.Log (N[eventList.Count]["type"]);

		List<string> BlueTeamWords = new List<string> ();
		List<string> RedTeamWords = new List<string> ();
		long lastTimestamp = (long)(eventList[eventList.Count - 1]["timestamp"]) - (long)TextCheckInterval;


		for (int i = 0; i < eventList.Count; i++) {
			if ((long)eventList [i]["timestamp"] > lastTimestamp) {	
				if (eventList [i]["team"] == 0) {
					BlueTeamWords.Add (eventList [i] ["type"]);
				} else {
					RedTeamWords.Add (eventList [i] ["type"]);
				}
			}
		}
			
		//Debug.Log (BlueTeamWords.Count);
		//Debug.Log (RedTeamWords.Count);

		string RedResponse = "";
		string BlueResponse = "";

		if (BlueTeamWords.Count >= 1) {
			BlueResponse = BlueTeamWords.GroupBy (i => i).OrderByDescending (grp => grp.Count ()).Select (grp => grp.Key).First ();
		} else {
			BlueResponse = "No response";
		}


		if (RedTeamWords.Count >= 1) {
			RedResponse = RedTeamWords.GroupBy (i => i).OrderByDescending (grp => grp.Count ()).Select (grp => grp.Key).First ();
		} else {
			RedResponse = "No response";
		}

		//RedResponse = "TROLL";

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
			GameObject.Find("Wall_Spawner").GetComponent<WallSpawnerScript>().WallSpeed -= 0.75f;
		}
		else {
			GameObject.Find("Red_spawner").GetComponent<WallSpawnerScript>().WallSpeed -= 0.75f;
		}

		return;
	}

	void RunFast (int Team) {
		if (Team == 0) {
			GameObject.Find("Red_spawner").GetComponent<WallSpawnerScript>().WallSpeed += 0.75f;
		}
		else {
			GameObject.Find("Wall_Spawner").GetComponent<WallSpawnerScript>().WallSpeed += 0.75f;
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
