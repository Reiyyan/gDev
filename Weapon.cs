
using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;


    //Variables needed to make sure you can only shoot in direction player is facing
	//public int ArmOff;
    public float rotZRef;
    public float rotZNegRef;
    public bool fright;
	public ArmRotation armRotationReference;
	public bool shooting;
    
    //Variables to prevent firing behind weapon

    public Locator locatorReference;
    public float differenceRef;

    //Variables needed for graphics
	public Transform BulletTrailPrefab;
	public Transform hitPrefab;
	public Transform muzzleFlashPrefab;

	public float effectSpawnRate = 0;

	float timeToSpawnEffect = 0;
	float timeToFire = 0;
	Transform firePoint;


    //Projectile
    public Rigidbody2D Bullet;
    public float pForce;


    // Use this for initialization
    void Awake () {

		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null){
			Debug.LogError ("No Firepoint?! WHAT?!?!");
	}
}


	// Update is called once per frame
    void Update()
    {


        //Locator References
        differenceRef = locatorReference.difference;

        // ArmRotation References
      //  ArmOff = armRotationReference.rotationOffset;
        rotZRef = armRotationReference.rotZ;
        fright = armRotationReference.faceRight;
        rotZNegRef = armRotationReference.rotZNeg;


        if (fireRate == 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
               shooting = !shooting;
                if (shooting == true)
                {
                    if((differenceRef > 0 && fright == true) || (differenceRef < 0 && fright == false))
                    {
                    if ((rotZRef >= -90.0 && rotZRef <= 90.0)  && (fright == true))
                        shoot();

                    if ((rotZNegRef >= -90.0 && rotZNegRef <= 90.0) && (fright == false))
                        shoot();

                  }
              }

            }
        }
        else
        {
            if (Input.GetButton("Fire1") && (Time.time > timeToFire))
            {
                shooting = !shooting;
                timeToFire = Time.time + 1 / fireRate;

                if (shooting == true)
                {
                    if((differenceRef > 0 && fright == true) || (differenceRef < 0 && fright == false))
                         {

                        if ((rotZRef >= -90.0 && rotZRef <= 90.0) && (fright == true))
                            shoot();
                        if ((rotZNegRef >= -90.0 && rotZNegRef <= 90.0) && (fright == false))
                            shoot();
                    }
                }
            }

        }
    }

	void shoot(){

		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (transform.position.x, transform.position.y);
        
    
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);

		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*10, Color.cyan);

		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);

			Enemy enemy = hit.collider.GetComponent<Enemy>();
			if (enemy != null){
				enemy.DamageEnemy (Damage);
				Debug.Log ("We hit " +hit.collider.name + " and did " +Damage +" damage");
			}
		}

		if (Time.time >= timeToSpawnEffect) {

			Vector3 hitPos;
			Vector3 hitNormal;

			if(hit.collider == null){
				hitPos = (mousePosition-firePointPosition)*30;
				hitNormal = new Vector3 (9999,9999,9999);
			}
			else{
				hitPos = hit.point;
				hitNormal = hit.normal;
			}
			Effect(hitPos, hitNormal);
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
			StartCoroutine(armRotationReference.Recoil());
		}
	}

	void Effect(Vector3 hitPos, Vector3 hitNormal){

		Transform trail = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;

		LineRenderer lr = trail.GetComponent<LineRenderer>();
		if (lr != null){
			lr.SetPosition(0, firePoint.position);
			lr.SetPosition(1, hitPos);
		}

		Destroy (trail.gameObject, 0.04f);

		if (hitNormal != new Vector3(9999,9999,9999))
		{
			Transform hitParticle = Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.left, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}

        Transform clone = Instantiate (muzzleFlashPrefab, firePoint.position, transform.rotation) as Transform;
		//clone.parent = firePoint;
		float size = Random.Range (1.6f, 1.9f);
		clone.localScale = new Vector3(size, size, size);
		Destroy (clone.gameObject, 03f);
		
	}




}
