using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
    public GameObject hole = null;
    public GameObject cam = null;
    public GameObject startMat = null;
	public Text distance;
    public Text score;
    public Slider powerbar;
	public AudioClip hitSound = null;
    public AudioClip holeSound = null;
    private int strokes = 0;
	private bool isMoving = false;
	private bool increasing = true;
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
        distance.GetComponent<Text>().text = "Distance To Hole:" + distanceToHole;
        score.GetComponent<Text>().text = "Strokes:" + strokes;

		//Setup powerbar values
		powerbar.minValue = minHitPower;
		powerbar.maxValue = maxHitPower;
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
				ballRollTime += Time.deltaTime;
				if (ballRollTime > 1 && GetComponent<Rigidbody> ().velocity.magnitude <= 0.5) {
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
					isMoving = false;
				}
			} else {
				ballRollTime = 0;
			}
		}
        //Calculate distance to hole
        calculateHoleDistance();

        //Detect if ball is in the hole
		/*NEEDS WORK, ALWAYS HITS WIN CONDITION EVEN WHEN BALL IS LAUNCHED OFF LEVEL*/
		if (ball.transform.position.y < 0.0f) {
			resetBall ();
		}else if(ball.transform.position.y > 6.0f && ball.transform.position.y < 8.0f){
            playHoleSound();
            winHole ();
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
		//Update the stroke score text
		updateScore(strokes);

        //Reset the power  and power bar level to minimum default after hitting ball
        hitPower = minHitPower;
        //powerbar.value = hitPower / powerMultiplier;
        powerbar.value = hitPower;
        Debug.Log("HitPower Reset: " + hitPower);
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
		Debug.Log ("You Win! \n Stroke Count: " + strokes);

		//Reset Stroke Count
		strokes = 0;

		//Teleport ball to start mat position and stop its movement.
		ball.transform.position = new Vector3(startMat.transform.position.x, 9.0f, startMat.transform.position.z);
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

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
}
