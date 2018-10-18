using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

   Attach, drag and drop this script onto Triple_Shot_Powerup and Speed_Powerup
   This class moves the Triple_Shot_Powerup and Speed_Powerup and detects collision
   with other Game Objects such as Player.

*/



public class Powerup : MonoBehaviour {



    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int powerupID; // 0 = triple shot , 1 = speed boost, 2 = shields

    [SerializeField]
    private AudioClip clip;



    // Use this for initialization   
    void Start () 
    {

        //transform.position = new Vector3(00, 5.43f, 00);
       

    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();


    }

    void Movement()
    {

       transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Destroy Triple_Shot_Powerup and Speed_Powerup when Off
        // the negative Y-Axis

        if (transform.position.y <= -5.55)
        {

           Destroy(this.gameObject);

        } 

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with: " + other.name);

        // Check that collision is between this Powerup object and Player object

        // No need to use other.name as we have set Object name Tag in the inspector

        // Destroy this PowerUp Object
       
       

        if (other.tag == "Player")
        {
           
            /*

                Create the ref plyr to GameObject Player 
                So that you can access public Player methods.
        
            */


            Player plyr = other.GetComponent<Player>();

           

            // Check we have a player
            if (plyr != null)
             {
                if(powerupID == 0)
                {
                    // Call method TripleShotPowerUpOn() from Player
                    // and enable TripleShot lasers.
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                    plyr.TripleShotPowerUpOn();

                }
                else if(powerupID == 1)
                {
                    // Call method SpeedBoostOn() from Player
                    // and enable speedboost.
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                    plyr.SpeedBoostOn();

                }
                else if (powerupID == 2)
                {

                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                    plyr.ShieldOn();

                }


            }

            Destroy(this.gameObject);

        }

       

    }


}
