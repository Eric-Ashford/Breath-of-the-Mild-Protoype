using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    [SerializeField]
    private GameObject startMenu, creditsMenu;

    private void Start()
    {
        creditsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        startMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ReturnToMain()
    {
        creditsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
