using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

   Attach, drag and drop this script onto Enemy Prefab
   This class moves the Enemy and detects collision
   with other Game Objects such as Player.

*/




public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyExplosionPrefab;


    [SerializeField]
    private float _speed = 2.0f;

    private UIManager uimanager;

    [SerializeField]
    private AudioClip clip;

   



    private float BottomEdgeOfScreen = -5.99f;


    // Use this for initialization
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

       

        //audioclip.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

        Movement();

    }

    private void Movement()
    {

        // Translate moves the Enemy transform down Y distance
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When Enemy is Off the negative Y-Axis then reposition enemy at top
        // of the Positive Y-Axis


        if (transform.position.y < BottomEdgeOfScreen)
        {
            // Spawn Object at Random Position along the X-Axis & Y-Axis
            float spawnPointX = Random.Range(-9.69f, 9.69f); 
            float spawnPointY = Random.Range(5.87f, 5.91f); // At top of the Y-Axis

            // Place Enemy at top of the Y-Axis and at a top point on the X-Axis. 
            transform.position = new Vector3(spawnPointX, spawnPointY, transform.position.z);


        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {



        if (other.tag == "Laser")
        {


           if (other.transform.parent != null)
            {
               
                //Destroy(other.transform.parent.gameObject);
                Destroy(GameObject.FindWithTag("Laser"));

            }


            // Enemy has collided with laser 
            // So Enemy and laser get destroyed
        
            Destroy(other.gameObject);

            // Method call to Explode 
            Explode();
          
            uimanager.UpdateScore();

            AudioSource.PlayClipAtPoint(clip,Camera.main.transform.position);

            Destroy(this.gameObject);

        }
        else if (other.tag == "Player")
        {
            // Create a Ref plyr to Player.
            Player plyr = other.GetComponent<Player>();

            // Check we have Player
            if (plyr != null)
            {
                // Call method Damage() from Player
                // Player then deducts 1 from player life.

                plyr.Damage();

            }

            // Method call to Explode 
           
            Explode();

            // If the player has used up 3 lives, then destroy player.  
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
           
            Destroy(this.gameObject);

        }

      
    }

    private void Explode()
    {

      
        // Instantiate our Explosion Prefab 
        Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);


    }

}
