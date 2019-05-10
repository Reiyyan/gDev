using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] Backgrounds;						//Array (List) of all the back- and forgrounds to be parallaxed
	private float[] ParallaxScales;						//The proportion of the camera's movement to move the bg by
	public float Smoothing = 1f;						//How smooth the parallaxing will be. Make sure to set this above 0. (or no parallax)

	private Transform cam;								//Reference to the main camera transform
	private Vector3 previousCamPos; 					//Store position of camera in previous frame
	
														// Is called before Start(). CALL ALL LOGIC BEFORE START BUT AFTER GAMEOBEJCTS SET
														// good to assign variable like camera and references between scripts and objects
	void Awake () {
														//Setup the Cam reference
		cam = Camera.main.transform;
	}


														// Use this for initialization
	void Start () {
														//The previous frame had the current frame's position
		previousCamPos = cam.position;

														//Assigning corresponding parallax scales
		ParallaxScales = new float[Backgrounds.Length];
		for (int i = 0; i < Backgrounds.Length; i++) {
						ParallaxScales[i] = Backgrounds[i].position.z*-1;
				}
	}
	
														// Update is called once per frame
	void Update (){

														//for each backgroung
	for (int i = 0; i < Backgrounds.Length; i++) {
														//The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * ParallaxScales[i];

														//Set a target x position which is the current position + the parallax
			float backgroundTargetPosX = Backgrounds[i].position.x + parallax;

														// Create a target position which is the backgrounds current position with it's target x position
			Vector3 BackgroundTargetPos = new Vector3 (backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

														// Fade between current position and the target position using lerp
			Backgrounds[i].position = Vector3.Lerp (Backgrounds[i].position, BackgroundTargetPos, Smoothing * Time.deltaTime);
			}
														//set the previousCamPos to the camera's position at the end of the frame

		previousCamPos = cam.position;
}
}