using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public vThirdPersonCamera thirdPersonCamera;
    public vThirdPersonInput thirdPersonInput;
    public FreeFlyCamera freeFlyCamera;

    bool isFreeFly = false;

    void Awake() {
        thirdPersonInput = GetComponent<vThirdPersonInput>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFreeFly)
            {
                thirdPersonCamera.enabled = true;
                thirdPersonInput.enabled = true;
                freeFlyCamera.enabled = false;
                isFreeFly = false;
                thirdPersonCamera.SetMainTarget(transform);
            }
            else
            {
                thirdPersonCamera.enabled = false;
                thirdPersonInput.enabled = false;
                freeFlyCamera.enabled = true;
                isFreeFly = true;
            }
        }
    }
}
