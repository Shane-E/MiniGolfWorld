using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Back : MonoBehaviour {
	public Button back;
	// Use this for initialization
	void Start () {
		back = GameObject.Find ("BackButton").GetComponent<Button> ();
		back.onClick.AddListener(() => backButtonClicked());
		Destroy(GameObject.Find("GameManager"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void backButtonClicked(){
		SceneManager.LoadScene ("1_MainMenu");
	}
}
