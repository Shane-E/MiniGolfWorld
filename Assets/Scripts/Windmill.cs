using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour {

    public GameObject windmillBlade;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(windmillBlade != null)
        {
            transform.Rotate(0, 0, Time.deltaTime * 50, Space.World);
        }
	}
}
