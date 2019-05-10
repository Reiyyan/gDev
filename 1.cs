using UnityEngine;
using System.Collections;

public class ArmRotation2 : MonoBehaviour {
	
	public int rotationOffset = 90;
	public float move;
	public PlatformerCharacter2D playerFace;
	public bool faceRight = true;
	public float OtherScript;
	public GameObject player;
	private PlatformerCharacter2D playerScript;
	
	
	void Start () {
		playerScript = player.GetComponent<PlatformerCharacter2D>();
	}
	// Update is called once per frame
	void Update () {
		
		if (playerScript.facingRight == true)
			faceRight = true;
		
		if (playerScript.facingRight == false)
			faceRight = false;
		print ("Facing Right:"+playerScript.facingRight); 
		print ("Face Right is also :"+faceRight); 
		
		
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;					//Subtracting position of player from the mouse position
		difference.Normalize();									//Normalizing the vector, meaning that the sum of the vector will be equal to 1
		
		float rotZ =  Mathf.Atan2 (difference.y, difference.x) *Mathf.Rad2Deg;			// Find the angle in degrees
		
		if ( ((rotZ >= 0.0 && rotZ <= 90.0) || (rotZ <= 0.0 && rotZ >= -90.0)) && (faceRight == true) )
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
		
		if ( ((rotZ > 90.0 && rotZ <= 180.0) || (rotZ <= -90.0 && rotZ >= -180.0)) && (faceRight == false) )
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
		
		if (faceRight == false)
			Flip ();
		
		
	}
	
	
	void Flip ()
	{
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
}
