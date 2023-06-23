using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public GameObject shopMenu;

    
    private bool isPaused = false;
    private bool isShop = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShop==false)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        //Time.timeScale = isPaused ? 0 : 1; // Pause or unpause the game
        pauseMenu.SetActive(isPaused); // Show or hide the pause menu
    }

    public void ToggleShop()
    {
        isShop = !isShop;
        //Time.timeScale = isShop ? 0 : 1; // Pause or unpause the game
        shopMenu.SetActive(isShop); // Show or hide the shop menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
