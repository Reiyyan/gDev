using UnityEngine;
using System.Collections;

public class LineRending : MonoBehaviour {

	LineRenderer shotLine;
	SpriteRenderer frontSpriteRenderer;

	// Use this for initialization
	void Start () {
	
		shotLine = GetComponent<LineRenderer>();
		frontSpriteRenderer = GetComponent<SpriteRenderer>();
		shotLine.sortingLayerID = frontSpriteRenderer.sortingLayerID;
		shotLine.sortingOrder = frontSpriteRenderer.sortingOrder;

	}
}