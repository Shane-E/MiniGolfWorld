using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour {

    public GameObject windmillBlade;
	public float bladeRotateSpeed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bladeRotateSpeed = Time.deltaTime * 75;

	    if(windmillBlade != null)
        {
			transform.Rotate(0, 0, bladeRotateSpeed, Space.World);
        }
	}
}
