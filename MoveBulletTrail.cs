﻿using UnityEngine;
using System.Collections;

public class MoveBulletTrail : MonoBehaviour {

	public int bulletMoveSpeed = 230;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * bulletMoveSpeed);	
		Destroy (gameObject, 1);
	}
}
