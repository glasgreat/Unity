using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {

        // Destroy object after 4 seconds.
        Destroy(this.gameObject, 4f);
		
	}
	

}
