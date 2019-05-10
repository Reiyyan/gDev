using UnityEngine;
using System.Collections;

public class ArmRotationTester : MonoBehaviour {

	public static int rotationOffset = 90;
	public bool faceRight = true;
	public GameObject player;
	private PlatformerCharacter2D playerScript;
	public float xOffSet = 0f;
	public float yOffSet = 0f;

	void Start () {
		playerScript = player.GetComponent<PlatformerCharacter2D>();

	}
	// Update is called once per frame
	void Update () {

		if (playerScript.facingRight == true)																		//Find out if player if going right or left
			faceRight = true;
		
		if (playerScript.facingRight == false)
			faceRight = false;
		print ("Facing Right:"+playerScript.facingRight); 
		print ("Face Right is also :"+faceRight); 


		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - (transform.position + new Vector3 (xOffSet,yOffSet,0));					//Subtracting position of player from the mouse position and Modified to increase range  (X,Y Offsets)
		difference.Normalize();																							//Normalizing the vector, meaning that the sum of the vector will be equal to 1



		float rotZ =  Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;			// Find the angle in degrees


		if ( ((rotZ >= 0.0 && rotZ <= 90.0) || (rotZ <= 0.0 && rotZ >= -90.0)) && (faceRight == true) )
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);


		float rotZNeg =  Mathf.Atan2 (difference.y, -difference.x) * Mathf.Rad2Deg;

	
		if ( ((rotZNeg > 0.0 && rotZNeg <= 90.0) || (rotZNeg <= 0.0 && rotZNeg >= -90.0)) && (faceRight == false) )			//Inversing rotation when player facing left
			transform.rotation = Quaternion.Euler (0f, 0f, rotZNeg + rotationOffset);
        
        {

		
		}

	}
	
}
