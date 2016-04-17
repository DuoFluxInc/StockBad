using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    public GameObject autors;
    public GameObject pron;

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
    public void Pron()
    {
        pron.SetActive(!pron.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
