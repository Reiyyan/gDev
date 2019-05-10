using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset;
    public int recoilValue;
    private int rotOriginal;
	public bool faceRight = true;
    public float rotZ;
    public float rotZNeg;
	public GameObject player;
	private PlatformerCharacter2D playerScript;
	public Weapon weaponReference;

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


		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;					//Subtracting position of player from the mouse position
		difference.Normalize();																							//Normalizing the vector, meaning that the sum of the vector will be equal to 1

		rotZ =  Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;			// Find the angle in degrees


		if ( ((rotZ >= 0.0 && rotZ <= 90.0) || (rotZ <= 0.0 && rotZ >= -90.0)) && (faceRight == true) )
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);


		rotZNeg =  Mathf.Atan2 (difference.y, -difference.x) * Mathf.Rad2Deg;

	
		if ( ((rotZNeg > 0.0 && rotZNeg <= 90.0) || (rotZNeg <= 0.0 && rotZNeg >= -90.0)) && (faceRight == false) )			//Inversing rotation when player facing left
			transform.rotation = Quaternion.Euler (0f, 0f, rotZNeg + rotationOffset);
        
       }

	public IEnumerator Recoil(){
        rotOriginal = rotationOffset;
		rotationOffset = recoilValue;
		yield return new WaitForSeconds (0.1f);
		rotationOffset = rotOriginal;
		yield return new WaitForSeconds (0.1f);
	}
	
}
