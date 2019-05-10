using UnityEngine;
using System.Collections;

public class DestroyThisSP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Destroy (gameObject, 2f);
	}
}