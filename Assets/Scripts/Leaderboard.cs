using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {
	CanvasGroup cg;
	public Text name, hole1Score, hole2Score, hole3Score, hole4Score, totalScore;
	private Manager gameManager;
	private int lastLevelIndex;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (GameObject.FindGameObjectWithTag("IngameCanvas"));
		gameManager = GameObject.Find ("GameManager").GetComponent<Manager> ();

		//Setup leaderboard and set it to be invisible and disabled
		cg = GetComponent<CanvasGroup>();
		cg.alpha = 0;
		cg.interactable = false;

		//Setup texts to enable manipulating of fields.
		name.GetComponent<Text>().text = PlayerPrefs.GetString("playerName");

		lastLevelIndex = gameManager.getTotalLevels () - 2;

		//Add restart button logic after game is over
		if (gameManager.getCurrentLevel () == lastLevelIndex) {
			gameManager.restart = GameObject.Find ("ReturnToMainMenu").GetComponent<Button>();
			gameManager.restart.onClick.AddListener(() => gameManager.restartGame());
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Show or hide the game scoreboard.
		displayScoreboard();

		//Show the scoreboard at the end of the game
		if (gameManager.getCurrentLevel () == lastLevelIndex) {
			cg.alpha = 1.0f;
			Destroy(GameObject.FindGameObjectWithTag("IngameCanvas"));
			gameManager.scoreboard = GameObject.Find ("Leaderboard").GetComponent<Leaderboard> ();
			gameManager.scoreboard.endGameLeaderboard ();
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

	public void updateLeaderboard(int level, int score, int total){
		Debug.Log ("Updating scoreboard hole " + level + " to score of " + score);
		Debug.Log ("Total Score: " + PlayerPrefs.GetInt("totalScore"));
		gameManager.scoreboard.GetComponent<Leaderboard> ().totalScore.text = total.ToString ();
		switch (level)
		{
		case 1:
			gameManager.scoreboard.GetComponent<Leaderboard> ().hole1Score.text = score.ToString();
			break;
		case 2:
			//Update the score text color to green for the previous hole.
			gameManager.scoreboard.GetComponent<Leaderboard>().hole1Score.color = new Color(0f, 0.94f, 0.07f);

			gameManager.scoreboard.GetComponent<Leaderboard> ().hole2Score.text = score.ToString();
			break;
		case 3:
			//Update the score text color to green for the previous hole.
			gameManager.scoreboard.GetComponent<Leaderboard>().hole2Score.color = new Color(0f, 0.94f, 0.07f);

			gameManager.scoreboard.GetComponent<Leaderboard> ().hole3Score.text = score.ToString();
			break;
		case 4:
			//Update the score text color to green for the previous hole.
			gameManager.scoreboard.GetComponent<Leaderboard>().hole3Score.color = new Color(0f, 0.94f, 0.07f);

			gameManager.scoreboard.GetComponent<Leaderboard> ().hole4Score.text = score.ToString();
			break;
		default:
			gameManager.scoreboard.GetComponent<Leaderboard> ().hole1Score.text = "0";
			gameManager.scoreboard.GetComponent<Leaderboard> ().hole2Score.text = "0";
			gameManager.scoreboard.GetComponent<Leaderboard> ().hole3Score.text = "0";
			gameManager.scoreboard.GetComponent<Leaderboard> ().hole4Score.text = "0";
			break;
		}
	}

	public void endGameLeaderboard(){
		gameManager.scoreboard.GetComponent<Leaderboard> ().totalScore.text = PlayerPrefs.GetInt("totalScore").ToString();

		gameManager.scoreboard.GetComponent<Leaderboard> ().hole1Score.text = PlayerPrefs.GetInt ("Hole1").ToString ();
		gameManager.scoreboard.GetComponent<Leaderboard> ().hole1Score.color = new Color (0f, 0.94f, 0.07f);

		gameManager.scoreboard.GetComponent<Leaderboard> ().hole2Score.text = PlayerPrefs.GetInt ("Hole2").ToString ();
		gameManager.scoreboard.GetComponent<Leaderboard> ().hole2Score.color = new Color (0f, 0.94f, 0.07f);

		gameManager.scoreboard.GetComponent<Leaderboard> ().hole3Score.text = PlayerPrefs.GetInt ("Hole3").ToString ();
		gameManager.scoreboard.GetComponent<Leaderboard> ().hole3Score.color = new Color (0f, 0.94f, 0.07f);

		gameManager.scoreboard.GetComponent<Leaderboard> ().hole4Score.text = PlayerPrefs.GetInt ("Hole4").ToString ();
		gameManager.scoreboard.GetComponent<Leaderboard> ().hole4Score.color = new Color (0f, 0.94f, 0.07f);
	}
}