using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] public static bool gameIsPaused = false;
    [SerializeField]    public GameObject pauseMenuUI;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape)) if (gameIsPaused) Resume(); else Pause();
    } 

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SaveState() 
    {   
        Game_Controller.instance.SaveState();
        Debug.Log("SALVO");
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
