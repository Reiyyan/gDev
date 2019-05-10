using UnityEngine;
using System.Collections;

public class Locator : MonoBehaviour {

    //Firepoint locator used to prevent backfire


    public float difference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        difference = mousePos.x - transform.position.x;
        }
}
