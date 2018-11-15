using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
    string sceneToLoad;
=======
    string MainSceneToLoad;
    [SerializeField]
    string CreditsSceneToLoad;
    [SerializeField]
    int startGameDelay = 5;
>>>>>>> 2060d5d4778a8d10a104106b21b93949462f2bc8

    //use when we have a loading scene
    //public void PlayButtonClicked()
    //{
    //    MenuMusic music = GameObject.Find("Menu Music").GetComponent<MenuMusic>();
    //    music.StopMusic();

    //    LoadingScene.LoadNewScene(sceneToLoad);
    //}

<<<<<<< HEAD
    public void MenuSceneButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
=======
    public void StartButtonClicked()
    {
        StartCoroutine(StartGameDelay());
    }

    public void CreditsButtonClicked()
    {
        SceneManager.LoadScene(CreditsSceneToLoad);
>>>>>>> 2060d5d4778a8d10a104106b21b93949462f2bc8
    }

    public void ExitGameButtonClicked()
    {
        Application.Quit();
    }
<<<<<<< HEAD
=======

    IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(startGameDelay);
        SceneManager.LoadScene(MainSceneToLoad);
    }
>>>>>>> 2060d5d4778a8d10a104106b21b93949462f2bc8
}
