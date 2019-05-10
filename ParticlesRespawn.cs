using UnityEngine;
using System.Collections;

public class ParticlesRespawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particles";
	}
}
