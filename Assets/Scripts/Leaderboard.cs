using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {
	CanvasGroup cg;
	public Text name, hole1Score, hole2Score, hole3Score, hole4Score, totalScore;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (GameObject.Find("Canvas"));

		//Setup leaderboard and set it to be invisible and disabled
		cg = GetComponent<CanvasGroup>();
		cg.alpha = 0;
		cg.interactable = false;

		//Setup texts to enable manipulating of fields.
		name.GetComponent<Text>().text = PlayerPrefs.GetString("playerName");
	}
	
	// Update is called once per frame
	void Update () {
		//Show or hide the game scoreboard.
		displayScoreboard();
	}

	void displayScoreboard(){
		//Slowly increasing and decreasing alpha creates a fade effect.
		if (Input.GetKey (KeyCode.Tab)) {
			cg.alpha += 0.1f;
		} else {
			cg.alpha -= 0.1f;
		}
	}
}
