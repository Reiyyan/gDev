using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {

    // Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
                            
        transform.position = new Vector3 (mousePos.x , mousePos.y, 0);

	}
}
