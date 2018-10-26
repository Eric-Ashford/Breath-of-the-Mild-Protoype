using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuCanvas;

    bool isPaused;

    void Start()
    {
        isPaused = false;
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("J_Start1") || Input.GetButtonDown("J_Start2") || Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        menuCanvas.SetActive(true);
    }

    void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
        menuCanvas.SetActive(false);
    }

    public void ResumeButtonClicked()
    {
        Unpause();
    }

    public void MainMenuButtonClicked()
    {
        Unpause();

        //LoadingScene.LoadNewScene("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
