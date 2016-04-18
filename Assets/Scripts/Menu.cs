using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    public GameObject autors;
    public GameObject multiplayer;

    public void Start()
    {
        Application.targetFrameRate = 60;
    }

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
    public void Multiplayer()
    {
        multiplayer.SetActive(!multiplayer.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
