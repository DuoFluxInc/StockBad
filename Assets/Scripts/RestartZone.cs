using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartZone : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player")
        SceneManager.LoadScene(Application.loadedLevelName, LoadSceneMode.Single);
    }
}
