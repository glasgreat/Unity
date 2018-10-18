using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Class Player

    Attach, drag and drop laser and TripleshotLaser 
    into the Inspector's Triple Shot & Laser Prefab
    fields. 
*/



public class Player : MonoBehaviour {



    // Give this Player GameObject references for a laser and TripleshotLaser
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private GameObject[] engineDamage;


    // Get a handle to following objects

    private UIManager uimanager;

    private GameManager gManager;

    private SpawnManager spwnManager;

    private AudioSource audioclp;

    private int hitCount = 0;


    public bool canTripleShot = false;
    public bool hasCollectedSpeedPowerUp = false;
    public bool hasCollectedShieldPowerUp;


    [SerializeField]
    private float _playerSpeed = 5.0f;


    [SerializeField]

    private int playerLife = 3;


    //Cool down between shots
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    public bool audioPlay;

  

    /* 

        Define screen boundary to keep player within screen view.
        The Battleship goes off screen view at TranPosX and TransPosY
        It is repositioned off the opposite end of screen
        and them moves back onto the screen view. ( Wrap around effect )

    */

   private float TranPosX = 9.43f; //  Transform Position X-Axis when object is off screen
   private float TransPosY = 5.63f; // Transform Position Y-Axis when object is off screen


	
    // Use this for initialization
	void Start () 
    {


        // current position = new position

        transform.position = new Vector3(00, 00, 00);

        //  
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        audioclp = GetComponent<AudioSource>();

        if(uimanager != null)
        {
            // Update player life 
            uimanager.UpdateLives(playerLife);

        }

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        spwnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(spwnManager != null)
        {
            spwnManager.StartSpawnRoutines();
        }

        hitCount = 0;

       
	}

    //Update is called once per frame
    void Update()
    {

        Movement();

        EngineUpdate();
        
       

       // Use Mouse left click and spacebar to fire laser
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {

            Shoot();

        }


    }

    private void Shoot()
    {


        // Cooldown between shots
        if (Time.time > _canFire)
        {
            if(canTripleShot == true)
            {

                //Spawn Triple Laser

                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                audioclp.Play();

            }
            else
            {
                //Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                audioclp.Play();
            }
     
           _canFire = Time.time + _fireRate;

        }


    }


    private void Movement()
    {

        /* 

            Input.GetAxis() method for keyboard input along Vertical and Horizontal Axis
            Get keyboard input using keyboard arrow buttons and keys "w, a, s d " 
            see Project Settings Input -- Inspector

        */


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(hasCollectedSpeedPowerUp == true) // Speed power up
        {
            float playerSpeedPowerUp =0f;
            playerSpeedPowerUp += _playerSpeed *  2.5f;

            // Translate moves the transform by x distance along the x axis and by y distance along the y axis.
            transform.Translate(Vector3.right * playerSpeedPowerUp * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * playerSpeedPowerUp * verticalInput * Time.deltaTime);

        }
        else if(hasCollectedSpeedPowerUp == false) // Normal speed
        {
           // Translate moves the transform by x distance along the x axis and by y distance along the y axis.
            transform.Translate(Vector3.right * _playerSpeed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _playerSpeed * verticalInput * Time.deltaTime);

        }
   
        // Keep Spaceship bound within the Game Screen.

        if (transform.position.x >= TranPosX) // When SpaceShip is off Righthand Side of Game Screen
        {
            // Then reposition SpaceShip to the LEFTHAND SIDE of Game Screen
            transform.position = new Vector3(TranPosX * -1, transform.position.y, transform.position.z);
        }

        else if (transform.position.x <= TranPosX * -1) // When SpaceShip is off Lefhand side of Game Screen
        {
            // Then reposition SpaceShip to the RIGHTHAND SIDE of Game Screen
            transform.position = new Vector3(TranPosX, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= TransPosY) // When SpaceShip is off the Top of Game Screen
        {
            // Then reposition SpaceShip at BOTTOM of Game Screen
            transform.position = new Vector3(transform.position.x, TransPosY * -1, transform.position.z);
        }

        else if (transform.position.y <= TransPosY * -1) // When SpaceShip is off the bottom of Game Screen
        {
            // Then reposition SpaceShip at TOP of Game Screen
            transform.position = new Vector3(transform.position.x, TransPosY, transform.position.z);
        }

        //Debug.Log("Transform Position: " + transform.position + "Time elapsed:" + Time.time);


    }

    // Call Coroutine 
    public void TripleShotPowerUpOn()
    {

        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    // Coroutine 
    public IEnumerator TripleShotPowerDownRoutine()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;

    }

    public void SpeedBoostOn()
    {

        hasCollectedSpeedPowerUp = true;
        StartCoroutine(SpeedBoostDownRoutine());

    }

    // Coroutine 
   public IEnumerator SpeedBoostDownRoutine()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5.0f);

        hasCollectedSpeedPowerUp = false;

    }

    public void Damage()
    {
    

        if (!hasCollectedShieldPowerUp)
        {


            playerLife--;

            EngineDamage();

            if(playerLife <=3)
            {
                uimanager.UpdateLives(playerLife);
            }

           // uimanager.UpdateLives(playerLife);

            Debug.Log("Remaining Life:" + playerLife);

        }
        else if(hasCollectedShieldPowerUp)
        {

            hasCollectedShieldPowerUp = false;
            _shield.SetActive(false);
            EngineDamage();

        }

        if (playerLife < 1)
        {

            Explode();

            Destroy(this.gameObject);

            Debug.Log("Game over");

            uimanager.UpdateScore();
        }
       

    }

    private void Explode()
    {
        // Instantiate PlayerExplosionPrefab
        Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);

        gManager.gameOver = true;
        uimanager.ShowGameOverTitleScreen();
        
    

        Destroy(this.gameObject);



    }

    public void ShieldOn()
    {

        if (playerLife <= 2)
        {
            playerLife++;
            uimanager.UpdateLives(playerLife);


        }

        hasCollectedShieldPowerUp = true;
        _shield.SetActive(true);

      
       StartCoroutine(ShieldPowerUpDownRoutine());

    }

    // Coroutine 
    public IEnumerator ShieldPowerUpDownRoutine()
    {
       
        // Wait for 10 seconds
         yield return new WaitForSeconds(10.0f);

         hasCollectedShieldPowerUp = false;
         _shield.SetActive(false);

      
    }


    public void EngineDamage()
    {

        hitCount++;



        if (hitCount == 1)
        {
          
            engineDamage[0].SetActive(true);
           // EngineUpdate();


        }
        else if(hitCount == 2)
        {
         
            engineDamage[1].SetActive(true);

            // Reset hitcount
            hitCount = 0;
          

        }
      /*  else if(hitCount >= 3 )
        {
            engineDamage[0].SetActive(true);
            engineDamage[1].SetActive(true);
        }*/

        Debug.Log("hitcount:  " + hitCount);

    }

    public void EngineUpdate()
    {
        
        // Has collected shield so damage engine repair. Ship back to normal!
        if (hasCollectedShieldPowerUp.Equals(true))
        {
            engineDamage[0].SetActive(false);
            engineDamage[1].SetActive(false);

        }


    }



}

   


