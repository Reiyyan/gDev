﻿using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {
	public float torQ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		GetComponent<Rigidbody2D>().AddTorque (torQ);

	}
}
