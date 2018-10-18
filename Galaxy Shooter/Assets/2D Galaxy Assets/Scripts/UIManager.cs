using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] livesRemaining;

    public Image livesImagedisplay;

    public int score;

    // Create reference to Text
    public Text scoreText;

    public GameObject titleScreen;
    public GameObject gameOverTitleScreen;

	

    public void UpdateLives(int currentLives)
    {

       
        Debug.Log("Current Lives is:" + currentLives);

        // Here


        if(currentLives <=3)
        {

            livesImagedisplay.sprite = livesRemaining[currentLives];

        }



    }
    public void UpdateScore()
    {

        score += 10;
       

       scoreText.text = "Score: " + score;

    }

    public void ResetScore()
    {


        // int resetScore = score;

        score = 0;

        scoreText.text = "Score: " + score;

    }

    public void DisplayScore()
    {

        scoreText.text = "Score: " + score;
    }


    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);

    }

    public void HideTitleScreen()
    {

        titleScreen.SetActive(false);
    }

    public void ShowGameOverTitleScreen()
    {
       
        gameOverTitleScreen.SetActive(true);

    }


    public void HideGameOverTitleScreen()
    {

        gameOverTitleScreen.SetActive(false);
    }



   



}
