using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    //use when we have a loading scene
    //public void PlayButtonClicked()
    //{
    //    MenuMusic music = GameObject.Find("Menu Music").GetComponent<MenuMusic>();
    //    music.StopMusic();

    //    LoadingScene.LoadNewScene(sceneToLoad);
    //}

    public void MenuSceneButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGameButtonClicked()
    {
        Application.Quit();
    }
}
