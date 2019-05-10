using UnityEngine;
using System.Collections;

public class MuzzleDirection : MonoBehaviour {

    public PlatformerCharacter2D pChar2DRef;

    public bool faceRight;
    public bool muzRight = true;
    public Component parSys;

	// Use this for initialization
	void Start () {

        parSys = GetComponent<ConstantForce>();


    }
	
	// Update is called once per frame
	void Update () {

        faceRight = pChar2DRef.facingRight;
        print("HEY REI" + faceRight);

        if (faceRight == false && muzRight == true) {
            transform.Rotate(0, 180, 0);
            muzRight = false;        
        }

        if (faceRight == true && muzRight == false)
        {
            transform.Rotate(0, 180, 0);
            muzRight = true;
        }
        
	}
}
