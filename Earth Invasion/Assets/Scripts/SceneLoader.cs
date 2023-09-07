using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
   

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        
    }


    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
