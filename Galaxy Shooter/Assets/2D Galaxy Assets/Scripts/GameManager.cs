using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;

    public GameObject player;

    public GameObject galaxy;

    private UIManager uiManager;

    private AudioSource MusicBackGround;

   

    private void Start()
    {
       
        // Handle to TitleScreen
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        MusicBackGround = Camera.main.GetComponent<AudioSource>();



        //uiManager.DisplayScore();
        PauseGame();
        Destroy(GameObject.FindWithTag("Enemy"));



    }



    private void Update()
    {
       
        if (gameOver == true)
        {

            PauseGame();
            PauseBackGroundMusic();
            uiManager.DisplayScore();
            Destroy(GameObject.FindWithTag("Enemy"));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(GameObject.FindWithTag("Enemy"));
                UnPauseGame();
                UnpauseBackGroundMusic();
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                uiManager.ResetScore();
                uiManager.HideTitleScreen();
                uiManager.HideGameOverTitleScreen();
            }
           


        }



        if (Input.GetKeyDown(KeyCode.P))
        {

            PauseGame();
            PauseBackGroundMusic();

        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {

             UnPauseGame();
             UnpauseBackGroundMusic();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 


    }

    private void PauseGame()
    {
       
            Time.timeScale = 0.0f;
            PauseBackGroundMusic();          
           
    }

   
    private void UnPauseGame()
    {
    
            //UnPauseGame();
            Time.timeScale = 1;
            UnpauseBackGroundMusic();
  
    }

    private void PauseBackGroundMusic()
    {

        MusicBackGround.Pause();

    }

    private void UnpauseBackGroundMusic()
    {

        MusicBackGround.UnPause();

    }



}
