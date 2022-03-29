using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //function to load the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //function to load the game scene
    public void Game()
    {
        SceneManager.LoadScene("Fight");
    }
}
