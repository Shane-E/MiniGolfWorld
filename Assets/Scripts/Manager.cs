using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	public InputField nameInput = null;
	string playerName;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

		Debug.Log ("Total Scenes: " + SceneManager.sceneCountInBuildSettings);

		//Debug.Log (SceneManager.GetActiveScene ().buildIndex);
		//Check if the scene is the main menu
		if (getCurrentLevel() == 0) {
			//Delete previously stored player name to create a new player.
			PlayerPrefs.DeleteKey ("playerName");
			PlayerPrefs.DeleteKey ("totalScore");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == (SceneManager.sceneCountInBuildSettings - 1)) {

		}
	}

	//Changes the level to the string named scene defined in the parameter.
	public void changeLevel(int levelName){
		SceneManager.LoadScene (levelName);
	}

	public void startButtonClicked(){
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
}
