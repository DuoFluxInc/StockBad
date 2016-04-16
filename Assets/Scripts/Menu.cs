using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    public GameObject autors;

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void Settings()
    {
        settings.SetActive(!settings.activeSelf);
    }
    public void Autors()
    {
        autors.SetActive(!autors.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
