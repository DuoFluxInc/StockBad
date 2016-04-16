using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    public Texture2D CrossHair;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, CrossHair.width, CrossHair.height), CrossHair);
    }
}
