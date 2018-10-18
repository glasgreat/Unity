using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour 
{
    [SerializeField]
    private GameObject enemyShipPrefab;


    //Create array to hold 3 prefabs: Triple_Shot_Powerup, Speed_Powerup and Shield_Powerup

    [SerializeField]
    private GameObject[] powerUps;  // Reference called powerUps to my Array of 3 Prefabs

    private GameManager gManager;
	


    // Use this for initialization
	void Start () 
    {

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Call co-routines to start spawning enemy and powerUps
        StartCoroutine(EnemySpawnDelay());
        StartCoroutine(PowerUpSpawnDelay());


    }

    public void StartSpawnRoutines()
    {

        StartCoroutine(EnemySpawnDelay());
        StartCoroutine(PowerUpSpawnDelay());

    }

    // Co-routine to spawn Enemy 
    public IEnumerator EnemySpawnDelay()
    {

        while(gManager.gameOver == false)
        {
        
            float spawnPointX = Random.Range(-9.69f, 9.69f);
            float spawnPointY = Random.Range(5.87f, 5.91f); // At top of the Y-Axis

            // Spwan new Enemy every 3 to 8 seconds.
            //float randomWait = Random.Range(3.0f, 8.0f);

            Instantiate(enemyShipPrefab, new Vector3(spawnPointX, spawnPointY), Quaternion.identity);

            yield return new WaitForSeconds(5.0f);


        }

    }
    // Co-routine to spawn powerUps from array
    public IEnumerator PowerUpSpawnDelay()
    {


        while(gManager.gameOver == false)
        {
            float spawnPointX = Random.Range(-9.69f, 9.69f);
            float spawnPointY = Random.Range(5.87f, 5.91f); // At top of the Y-Axis


            //  Store random range for powerUps
            int randomPowerUp = Random.Range(0, 3);

            // Instantiate new powerUps at a random location eachtime the while loop runs.
            Instantiate(powerUps[randomPowerUp], new Vector3(spawnPointX, spawnPointY), Quaternion.identity);
          
            yield return new WaitForSeconds(5.0f);

        }


       

    }




}
