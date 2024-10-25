using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;
    public GameObject PauseMenuUI;
    public GameObject Death;
    bool CanPause = true;
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if 
            (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
   public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause ()
    {
        if (CanPause)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
        }
       
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuFixed");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    public void Gameover()
    {
        CanPause = false;
        Death.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = false;
    }
}
