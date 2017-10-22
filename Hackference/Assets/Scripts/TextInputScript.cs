using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.json;

public class TextInputScript : MonoBehaviour {

	float TextCheckInterval, timeCounter;
	bool CheckReady;
	public GameObject FlashBackground;

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

			switch (MostPopularStrings [0]) {
				case "FLASH":
					RunFlash(0);
					break;
				case "SLOW":
					RunSlow(0);
					break;
				case "FAST":
					RunFast(0);
					break;
				case "PUPPIES":
					RunPuppies(0);
					break;
				case "KITTENS":
					RunKittens(0);
					break;
				case "TROLL":
					RunTroll(0);
					break;
				default:
					Debug.Log ("No response");
					break;
				}



		} else {
			timeCounter += Time.deltaTime;
			if (timeCounter >= TextCheckInterval) {
				CheckReady = true;
			}
		}
	}

	List<string> DoTextCheck () {
		string text = System.IO.File.ReadAllText(@"C:\Users\Rosanna\Documents\GitHub\HackferenceProject\number.txt");

		Debug.Log (text);

		List<string> MostPopularStrings = new List<string> (2);
		MostPopularStrings.Add ("FLASH"); //Blue Team Response
		MostPopularStrings.Add ("FLASH"); //Red Team Response
		return MostPopularStrings;
	}


	void RunFlash (int Team) {
		if (Team == 0) {
			Instantiate(FlashBackground, new Vector3 (0f, -3.2f, -1f), Quaternion.identity, transform);
		}
		else {
			Instantiate(FlashBackground, new Vector3 (0f, 3.2f, -1f), Quaternion.identity, transform);
		}
	}

	void RunSlow (int Team) {
		return;
	}

	void RunFast (int Team) {
		return;
	}

	void RunPuppies (int Team) {
		return;
	}

	void RunKittens (int Team) {
		return;
	}

	void RunTroll (int Team) {
		return;
	}

}
