using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	public InputField nameInput = null;
	public Leaderboard scoreboard;
	public Button restart;
	string playerName, holeName;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

		Debug.Log ("Total Scenes: " + SceneManager.sceneCountInBuildSettings);

		//Debug.Log (SceneManager.GetActiveScene ().buildIndex);
		//Check if the scene is the main menu
		if (getCurrentLevel() == 0) {
			//Delete previously stored player name to create a new player.
			//PlayerPrefs.DeleteKey ("playerName");
			//PlayerPrefs.DeleteKey ("totalScore");
			PlayerPrefs.DeleteAll();
		}
		holeName = "Hole" + (getCurrentLevel () + 1);
		Debug.Log ("Hole Name: " + holeName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Changes the level to the string named scene defined in the parameter.
	public void changeLevel(int levelName){
		SceneManager.LoadScene (levelName);
		holeName = "Hole" + (getCurrentLevel () + 1);
		Debug.Log ("Hole Name: " + holeName);
	}

	public void startButtonClicked(){
		PlayerPrefs.DeleteAll();
		//Get and store the new player name.
		if (nameInput.text == "" || nameInput.text == null) {
			Debug.Log ("No Input.");
			//Set a default player name.
			PlayerPrefs.SetString ("playerName", "Player 1");
		} else {
			playerName = nameInput.text;
			Debug.Log ("Player Name: " + playerName);
			PlayerPrefs.SetString ("playerName", playerName);
		}
		//Change to first level
		changeLevel (1);
		
	}

	//Load the instructions scene
	public void instructionsButtonClicked(){
		SceneManager.LoadScene ("7_Instructions");
	}

	public void restartGame(){
		Destroy (GameObject.Find("GameManager")); //Cleanup leftover game manager from previous game
		changeLevel (0);
	}

	public string getPlayerName(){
		return playerName;
	}

	public int getCurrentLevel(){
		return SceneManager.GetActiveScene ().buildIndex;
	}

	public int getTotalLevels(){
		int totalLevels = SceneManager.sceneCountInBuildSettings;
		return totalLevels;
	}

	public string getHoleName(){
		return holeName;
	}
}
