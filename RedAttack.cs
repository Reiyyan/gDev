using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedAttack : MonoBehaviour {

    public Transform blueTarget;
    private Vector2 bPosition;
    private Vector2 rPosition;

    //RandomShot
    public float rng;
    
    //aiming and list creation
    private Collider2D BlueTargetCol;
    public GameObject BlueTarGO;
    public List<GameObject> BlueTargetsL = new List<GameObject>();

    public LayerMask whatToHit;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    private bool searchingForBlue = false;

    //Projectile
    public Rigidbody2D rMagic;
    public float pForce;

    //AttackStance
    public bool Attacking;

	// Use this for initialization
	void Start () {

	}
    void OnTriggerEnter2D(Collider2D BlueTargetCol)
    {
        if (BlueTargetCol.gameObject.tag == "Blue Team")
        {
            Debug.LogError("Target is going to be " + BlueTargetCol.gameObject.name);
            BlueTarGO = BlueTargetCol.gameObject;
            if (!BlueTargetsL.Contains(BlueTarGO)) { BlueTargetsL.Add(BlueTarGO); }
            StartCoroutine(SearchForTarget());


        }

       // foreach (GameObject blueOb in BlueTargetsL) { Debug.LogError("LIST PRINTING" +blueOb.name); }
    }

    void OnTriggerExit2D(Collider2D BlueTargetCol)
    {
        if (BlueTargetCol.gameObject.tag == ("Blue Team"))
        {
            Debug.LogError("NO TARGET NOW");
            BlueTarGO = BlueTargetCol.gameObject;
            BlueTargetsL.Remove(BlueTarGO);
            BlueTarGO = null;
            blueTarget = null;

        }
    }

    IEnumerator SearchForTarget()
    {
        GameObject sBResult = BlueTargetsL[0].gameObject;

        if (sBResult == null)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(SearchForTarget());
        }
        else
        {
            searchingForBlue = false;
            blueTarget = sBResult.transform;
            StartCoroutine(UpdateTarget());
            yield return false;
        }
        
    }

    IEnumerator UpdateTarget()
    {
        if (blueTarget == null)
        {
            if (!searchingForBlue)
            {
                searchingForBlue = true;
                StartCoroutine(SearchForTarget());
            }
        }

        yield return false;
        StartCoroutine(UpdateTarget());
    }

    void Shoot()
    {
        bPosition = new Vector2(blueTarget.transform.position.x, (blueTarget.transform.position.y+rng)); //add RNG code
        rPosition = new Vector2(transform.position.x, transform.position.y);
       // RaycastHit2D hit = Physics2D.Raycast(rPosition, bPosition - rPosition, 100, whatToHit);

        if (Time.time >= timeToSpawnEffect)
        {
            Projectile();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;

        }
    }


    void Projectile()
    {
        Rigidbody2D clone;
        clone = Instantiate(rMagic, transform.position, rMagic.transform.rotation) as Rigidbody2D;
        //clone.velocity = transform.TransformDirection((bPosition - rPosition) * 1);
        clone.AddForce((bPosition - rPosition) * pForce);
        //setting to be a child of this GameObject
        clone.transform.parent = this.transform;

    }
    
  
    // Update is called once per frame
    void Update()
    {
        
        if (blueTarget == true)
        {
            Attacking = true;
            rng = Random.Range(-1F, 2.8F);
            Shoot();
           // Debug.LogError("IN RANGE and shooting");
        }

        if (blueTarget == null)
        {
            Attacking = false;
        }

        while (BlueTargetsL.Remove(null));
    }
}
