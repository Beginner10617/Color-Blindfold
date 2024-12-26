using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        player.GetComponent<PixelPerfectMovement>().enabled = false;
        player.GetComponent<HealthSystem>().enabled = false;
        pauseButton.SetActive(false);
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        player.GetComponent<PixelPerfectMovement>().enabled = true;
        player.GetComponent<HealthSystem>().enabled = true;
        pauseButton.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }   
    }
}
