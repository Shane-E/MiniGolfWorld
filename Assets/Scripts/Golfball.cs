using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
    public GameObject hole = null;
    public GameObject cam = null;
    public GameObject startMat = null;
	public GameObject ballPointer = null;
	private Manager gameManager;
	public Text distance;
    public Text score;
	public Text name;
    public Slider powerbar;
	public AudioClip hitSound = null;
    public AudioClip holeSound = null;
	//public Leaderboard scoreboard;
	private int scoreTotal;
    private int strokes = 0;
	private bool isMoving = false;
	private bool increasing = true;
	private bool holeWon = false;
    private float distanceToHole;
	public float minHitPower = 5.0f;
    public float maxHitPower = 85.0f;
	private float hitPower = 0;
	private float powerIncrement = 2.0f;
	//private float powerMultiplier = 10;
	private float ballRollTime = 0;
    private Vector3 ballDir;

    // Use this for initialization
    void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<Manager> ();
		distance = GameObject.Find ("DistanceToHole").GetComponent<Text> ();
		score = GameObject.Find ("Score").GetComponent<Text> ();
		name = GameObject.Find ("Name").GetComponent<Text> ();
		powerbar = GameObject.Find ("PowerSlider").GetComponent<Slider> ();
		gameManager.scoreboard = GameObject.Find ("Leaderboard").GetComponent<Leaderboard> ();

        distance.GetComponent<Text>().text = "Distance To Hole:" + distanceToHole;
        score.GetComponent<Text>().text = "Strokes:" + strokes;
		name.GetComponent<Text> ().text = PlayerPrefs.GetString("playerName");

		//Setup powerbar values
		powerbar.minValue = minHitPower;
		powerbar.maxValue = maxHitPower;

		//Ball pointer display
		ballPointer.SetActive (true);


		scoreTotal = PlayerPrefs.GetInt("totalScore");
		gameManager.scoreboard.updateLeaderboard (gameManager.getCurrentLevel(), strokes, scoreTotal);

		Debug.Log ("Beginning of level total: " + scoreTotal);
		//Debug.Log ("Hole Y Position: " + hole.transform.position.y);
		//Debug.Log ("Hole X Position: " + hole.transform.position.x);
    }
	
	// Update is called once per frame
	void Update () {
        //Allow the ball to be hit if the ball is not null, not currently moving, and the left mouse button is clicked.
        if (ball != null) {
            if (Input.GetButton("Fire1") && !isMoving) {
				calculatePower ();
            }

            //Hit ball using power level and set ball to moving.
            if (Input.GetButtonUp("Fire1") && !isMoving)
            {
                //Calculate direction to hit ball
                ballDir = cam.transform.forward.normalized;
                hitBall(hitPower);
                isMoving = true;
            }

			//Detect when the ball stops
			if (isMoving) {
				//The ball pointer is invisible while the ball is in motion
				ballPointer.SetActive (false);
				ballRollTime += Time.deltaTime;
				if (ballRollTime > 1 && GetComponent<Rigidbody> ().velocity.magnitude <= 0.5) {
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
					isMoving = false;
				}
			} else {
				//View the pointer when the ball has stopped.
				ballPointer.SetActive (true);
				ballRollTime = 0;
			}
		}
        //Calculate distance to hole
        calculateHoleDistance();

        //Detect if ball is in the hole
		if(Vector3.Distance(ball.transform.position, hole.transform.position) < 0.75f) {
			playHoleSound();
			winHole ();
		}else if (ball.transform.position.y < 0.0f){
			resetBall ();
			//TODO Play Sound For Out Of Bounds
		}
    }

	void calculatePower(){
        //Increase power if it is less than the max power.
        if (increasing)
        {
            if (hitPower < maxHitPower)
            {
                //hitPower += powerIncrement * powerMultiplier;
                hitPower += powerIncrement;
                increasing = true;
				//Increase difficulty for high power shots
				if(hitPower > maxHitPower/2){
					powerIncrement = 4;
				}else{
					powerIncrement = 2;
				}
            }
            else if (hitPower >= maxHitPower)
            {
                increasing = false;
            }
        }
		//Decrease power if power level is not increasing until the power hits the minimum level.
		if(!increasing) {
			//Debug.Log ("Not Increasing");
			if (hitPower > minHitPower) {
                //Debug.Log ("HitPower: " + hitPower);
                //hitPower -= powerIncrement * powerMultiplier;
                hitPower -= powerIncrement;
			} else if (hitPower <= minHitPower) {
				increasing = true;
			}
		}
        //Update the slider
        //powerbar.value = hitPower / powerMultiplier;
        powerbar.value = hitPower;
	}

	void hitBall (float power){

		//Play Hit Audio
		playHitSound();

        //Add force to the ball
        //ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, power));
        //Camera.main.transform.forward
        ballDir.y = 0;
		ball.GetComponent<Rigidbody>().AddRelativeForce(ballDir * power, ForceMode.Impulse);

        //Increase stroke count
        strokes++;

		//Update the stroke score text for quick UI and scoreboard
		updateScore(strokes);
		gameManager.scoreboard.updateLeaderboard (gameManager.getCurrentLevel (), strokes, scoreTotal);

        //Reset the power  and power bar level to minimum default after hitting ball
        hitPower = minHitPower;
        //powerbar.value = hitPower / powerMultiplier;
        powerbar.value = hitPower;
        //Debug.Log("HitPower Reset: " + hitPower);
    }

    void calculateHoleDistance()
    {
        distanceToHole = Mathf.Round((Vector3.Distance(ball.transform.position, hole.transform.position) * 100f) / 100f);
        distance.GetComponent<Text>().text = "Distance To Hole: " + distanceToHole;
    }

    void updateScore(int stroke)
    {
        score.GetComponent<Text>().text = "Strokes:" + stroke;
    }

    void resetBall()
    {
		Debug.Log ("Out of Bounds!");

        //Teleport ball to start mat position and stop its movement.
        ball.transform.position = new Vector3(startMat.transform.position.x, 9.0f, startMat.transform.position.z);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

	void winHole()
	{
		//Debug.Log ("You Win! \n Stroke Count: " + strokes);
		//Update and save total score
		scoreTotal += strokes;
		PlayerPrefs.SetInt("totalScore", scoreTotal);

		PlayerPrefs.SetInt (gameManager.getHoleName (), strokes);

		//Reset Stroke Count
		strokes = 0;

		//Conditional to tell when the game has ended to show final scores.
		if (gameManager.getCurrentLevel () < gameManager.getTotalLevels () - 1) {
			//Go to the next level
			Debug.Log("Changing to level " + (gameManager.getCurrentLevel() + 1));
			gameManager.changeLevel (gameManager.getCurrentLevel () + 1);
		} else {
			//Go to end scene
			Debug.Log("Changing to last level.");
			gameManager.changeLevel (gameManager.getTotalLevels() - 1);
		}
	}

	void playHitSound(){
		if (hitSound != null) {
			GetComponent<AudioSource> ().PlayOneShot (hitSound, 0.7f);
		} else {
			Debug.Log ("Error playing hit sound.");
		}
	}

    void playHoleSound()
    {
        if(holeSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(holeSound, 0.7f);
        }
        else
        {
            Debug.Log("Error playing hole sound");
        }
    }

	public bool getBallMovement(){
		return isMoving;
	}

	public bool getWinCondition(){
		return holeWon;
	}

	/*void updateLeaderboard(int level, int score, int total){
		Debug.Log ("Updating scoreboard hole " + level + " to score of " + score);
		Debug.Log ("Total Score: " + PlayerPrefs.GetInt("totalScore"));
		scoreboard.GetComponent<Leaderboard> ().totalScore.text = total.ToString ();
		switch (level)
		{
		case 1:
			scoreboard.GetComponent<Leaderboard> ().hole1Score.text = score.ToString();
			break;
		case 2:
			//Update the score text color to green for the previous hole.
			scoreboard.GetComponent<Leaderboard>().hole1Score.color = new Color(0f, 0.94f, 0.07f);

			scoreboard.GetComponent<Leaderboard> ().hole2Score.text = score.ToString();
			break;
		case 3:
			//Update the score text color to green for the previous hole.
			scoreboard.GetComponent<Leaderboard>().hole2Score.color = new Color(0f, 0.94f, 0.07f);

			scoreboard.GetComponent<Leaderboard> ().hole3Score.text = score.ToString();
			break;
		case 4:
			//Update the score text color to green for the previous hole.
			scoreboard.GetComponent<Leaderboard>().hole3Score.color = new Color(0f, 0.94f, 0.07f);

			scoreboard.GetComponent<Leaderboard> ().hole4Score.text = score.ToString();
			break;
		default:
			scoreboard.GetComponent<Leaderboard> ().hole1Score.text = "0";
			scoreboard.GetComponent<Leaderboard> ().hole2Score.text = "0";
			scoreboard.GetComponent<Leaderboard> ().hole3Score.text = "0";
			scoreboard.GetComponent<Leaderboard> ().hole4Score.text = "0";
			break;
		}
	}*/
}
