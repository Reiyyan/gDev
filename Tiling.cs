using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;					// The offset so we don't get weird errors

											// Used for checking if we need to instantiate land
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false;		// Used if the object is not tilable

	private float spriteWidth = 0f;			// The width of element
	private Camera cam;
	private Transform myTransform;

	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;

	}
	
	// Update is called once per frame
	void Update () {
											// Check for buddies
		if (hasALeftBuddy == false || hasARightBuddy == false) {
											// Calculate the cameras extnesion (half the width of what camera can see in world coordinates
				float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

											// Calculate the x position where the camera can see the edge of the sprite
				float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
				float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;


											// Checking if we can see the edge of the element and then calling make MakeNewBuddy if possible
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy(1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy(-1);
				hasALeftBuddy = true;
			}
		}
}
												
												// A function that creates a buddy on the side required
	void MakeNewBuddy (int rightOrLeft) {
												// Calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
												// We are instiating our new buddy and storing him in a new variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;
												// If not tilable let's reverse the x size of our object to get rid of ugly scene
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else{
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
		}
}