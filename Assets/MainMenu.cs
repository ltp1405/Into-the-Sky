using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject mainMenu;

    BasicBehaviour basicBehaviour;
    MoveBehaviour moveBehaviour;
    AimBehaviourBasic aimBehaviourBasic;
    FlyBehaviour flyBehaviour;
    ThirdPersonOrbitCamBasic thirdPersonOrbitCamBasic;

    void Awake()
    {
        basicBehaviour = player.GetComponent<BasicBehaviour>();
        moveBehaviour = player.GetComponent<MoveBehaviour>();
        aimBehaviourBasic = player.GetComponent<AimBehaviourBasic>();
        flyBehaviour = player.GetComponent<FlyBehaviour>();
        thirdPersonOrbitCamBasic = player.GetComponentInChildren<ThirdPersonOrbitCamBasic>();
    }

    public void EnablePlayerBehaviours()
    {
        basicBehaviour.enabled = true;
        moveBehaviour.enabled = true;
        aimBehaviourBasic.enabled = true;
        flyBehaviour.enabled = true;
        thirdPersonOrbitCamBasic.enabled = true;
    }

    void DisablePlayerBehaviours()
    {
        basicBehaviour.enabled = false;
        moveBehaviour.enabled = false;
        aimBehaviourBasic.enabled = false;
        flyBehaviour.enabled = false;
        thirdPersonOrbitCamBasic.enabled = false;
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
