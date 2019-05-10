using UnityEngine;
using System.Collections;

public class MotorSpeed : MonoBehaviour {

	public int mSpeed = 0;
	private HingeJoint2D ElbowJoint;


	// Use this for initialization
	void Start () {

		ElbowJoint = this.GetComponent<HingeJoint2D>();

	}
	
	// Update is called once per frame
	void Update () {
	
	
		print (""+ElbowJoint.motor.motorSpeed);
		print ("" + ElbowJoint.useLimits);
		ElbowJoint.useLimits = false;
		print ("now its" + ElbowJoint.useLimits);

	}
}
