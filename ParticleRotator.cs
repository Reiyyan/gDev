using UnityEngine;
using System.Collections;

public class ParticleRotator : MonoBehaviour {

    public GameObject casterAIReference;
    public float targetDisplacement;


    void Awake() {
     casterAIReference = GameObject.Find("Red Caster Minion");
 }


    void OnCollisionEnter2D(Collision2D collision2d)
    {
        if (collision2d.rigidbody == true)
            Destroy(this.gameObject, 0.02f);

    }


    // Update is called once per frame
    void Update () {

        targetDisplacement = casterAIReference.GetComponent<CasterAI>().xDistance;

        if(targetDisplacement > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);

        else if (targetDisplacement < 0)
            transform.eulerAngles = new Vector3(0, 0, 180);

      //  if (Collision.rigidbody)
         //   Destroy(this);

    }


}
