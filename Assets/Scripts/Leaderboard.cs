using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {
	CanvasGroup cg;
	public Text name, hole1Score, hole2Score, hole3Score, hole4Score, totalScore;
	private Manager gameManager;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (GameObject.Find("Canvas"));
		gameManager = GameObject.Find ("GameManager").GetComponent<Manager> ();

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
		if (gameManager.getCurrentLevel () == (gameManager.getTotalLevels () - 1)) {
			drawEndScoreboard ();
			cg.alpha = 1.0f;
		}
	}

	void displayScoreboard(){
		//Slowly increasing and decreasing alpha creates a fade effect.
		if (Input.GetKey (KeyCode.Tab)) {
			cg.alpha += 0.1f;
		} else {
			cg.alpha -= 0.1f;
		}
	}

	//Draw the background, game title, and return to main menu button
	void drawEndScoreboard(){

	}
}
