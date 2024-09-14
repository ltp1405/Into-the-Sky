using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3rdPerson+Fly 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
