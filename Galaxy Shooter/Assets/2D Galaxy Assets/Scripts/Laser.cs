using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

   Attach, drag and drop this script onto laser Prefab
   This class moves the laser up the Y-Axis

*/


public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 20.0f;


    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {

        moveLaser();
       

    }

    void moveLaser()
    {

        // Move laser up Y-Axis
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 5.43)
        {

            if(transform.position !=null)
             {
                //Destroy(transform.parent.gameObject);
                Destroy(GameObject.FindWithTag("Laser"));

             } 

            Destroy(this.gameObject);

        }




    }
}
