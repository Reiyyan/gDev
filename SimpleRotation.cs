using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 fireRotZ = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;					//Rotating firepoint due to errorous shots when flipped left
		fireRotZ.Normalize();	
		
		float rotSim =  Mathf.Atan2 (fireRotZ.y, fireRotZ.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.Euler (0f, 0f, rotSim);

	}
}
