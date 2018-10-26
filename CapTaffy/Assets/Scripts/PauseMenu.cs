using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuCanvas;

    bool isPaused;

    const string startButtonName = "Start";

    void Awake()
    {
        isPaused = false;
    }

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown(startButtonName))
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
