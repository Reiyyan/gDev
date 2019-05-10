using UnityEngine;
using System.Collections;

public class WeaponTest1 : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public Transform BulletContainerPrefab;
	public float reifloat = 0;


	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null){
			Debug.LogError ("No Firepoint?! WHAT?!?!");

	
	}
	}
	// Update is called once per frame
	void Update () {
	if (fireRate == 0){
			if(Input.GetButtonDown ("Fire1")){
				shoot();
			}
		}
		else{
			if(Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				shoot();
			}
		}
	}

	void shoot(){
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);

		Effect ();

		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);

		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " +hit.collider.name + " and did " +Damage +" damage");
		}
	}

	void Effect(){

		Vector3 jack = firePoint.rotation.eulerAngles;

		print ("in euler " +jack);
		print ("In q right now" + Quaternion.Euler (jack));

		Quaternion JackQ = Quaternion.Euler (-90 * jack.x, jack.y, jack.z);

		print ("in q " +JackQ);

		Instantiate (BulletContainerPrefab, firePoint.position, JackQ);
		print ("roation is" +firePoint.rotation);

		
	}
}
