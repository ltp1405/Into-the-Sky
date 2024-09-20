using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject mainMenu;
    private vThirdPersonInput vThirdPersonInput;

    void Awake()
    {
        vThirdPersonInput = player.GetComponent<vThirdPersonInput>();
    }

    public void EnablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = true;
    }

    void DisablePlayerBehaviours()
    {
        vThirdPersonInput.enabled = false;
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DisablePlayerBehaviours();
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        EnablePlayerBehaviours();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Playground++");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
