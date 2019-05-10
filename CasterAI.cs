using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]
public class CasterAI : MonoBehaviour {

	// What to chase
	public Transform target;

	// How many times each second we update the path
	public float updateRate = 2f;

	//Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//The AI's Speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

    //Finding out which way to face
    public float xDistance;

	[HideInInspector]
	public bool pathIsEnded = false;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWayPointDistance = 3;

	// The waypoint we are currently moving towards
	private int currentWayPoint = 0;

	private bool searchingForPlayer = false;

    //Referencing Attack Stance from Targeting Script
    public RedAttack redAttackReference;
    public bool attackingStance;

	void Start (){

        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		if (target == null){
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}

		// Start a new path to the targer position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath());
	}

	IEnumerator SearchForPlayer(){

		GameObject sResult = GameObject.FindGameObjectWithTag("Player");
		if (sResult == null){
			yield return new WaitForSeconds (1f);
			StartCoroutine(SearchForPlayer());
		}
		else {
			searchingForPlayer = false;
			target = sResult.transform;
			StartCoroutine(UpdatePath());
			yield return false;                                 //used yield return to remove error
		}
			
	}

	IEnumerator UpdatePath(){
		if (target == null){
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
            yield return false;                                 //used yield return to remove error
		}

		// Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position, target.position, OnPathComplete);
		
		yield return new WaitForSeconds (1f/updateRate);
		StartCoroutine (UpdatePath());
	}

	public void OnPathComplete (Path p){
		Debug.Log ("we got a path. Did it have an error?" +p.error);
		if (!p.error){
			path = p;
			currentWayPoint = 0;
			}
	}

	void FixedUpdate() {
		if (target == null){
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}

		//TODO: Always Look at Player.

		if (path == null) { return; }

		if (currentWayPoint >= path.vectorPath.Count)
        {
			if(pathIsEnded)
				return;
			Debug.Log ("End of Path Reached.");

			pathIsEnded = true;
		}

		pathIsEnded = false;

		//Direction to next waypoint
		Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

       //Distance between Unit and Target

       xDistance = (transform.position.x - target.position.x);
       

       //Finding out if we are in Attack Stance
       attackingStance = redAttackReference.Attacking;

        //Move the AI

        //Adding code to only add force if not attacking
        if (attackingStance != true)
        {
            rb.AddForce(dir, fMode);
            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);

            if (dist < nextWayPointDistance)
            {
                currentWayPoint++;
                return;
            }

        }

        if (xDistance >= 0)
            transform.localScale = new Vector3(-1, transform.localScale.y);
        else if (xDistance < 0)
            transform.localScale = new Vector3(1, transform.localScale.y);

        
	}
	
}
