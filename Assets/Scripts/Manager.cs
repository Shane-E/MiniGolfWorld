using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	public InputField nameInput = null;
	string playerName;

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey ("playerName");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Changes the level to the string named scene defined in the parameter.
	public void changeLevel(int levelName){
		SceneManager.LoadScene (levelName);
	}

	public void startButtonClicked(){
		playerName = nameInput.text;
		Debug.Log("Player Name: " + playerName);
		PlayerPrefs.SetString ("playerName", playerName);
		//Change to first level
		changeLevel (1);
	}

	public string getPlayerName(){
		return playerName;
	}
}
