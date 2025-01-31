﻿using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        public AudioSource runSoundSource;  
        public AudioClip runSound;          
        public float baseRunSoundPitch = 1f; 
        public float sprintPitchMultiplier = 1.5f; 
        public float speedThreshold = 0.1f; 
        public LayerMask obstacleLayers;     
        public float obstacleCheckDistance = 1f; 
        private Rigidbody rb;
        private bool isRunningSoundPlaying = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            ControlRunningSound();
        }
        public void StopRunningSound()
        {
            if (isRunningSoundPlaying)
            {
                runSoundSource.Stop();
                isRunningSoundPlaying = false;
            }
        }
        private void ControlRunningSound()
        {
            float speed = rb.velocity.magnitude;

            if (speed < speedThreshold || IsBlockedByObstacle())
            {
                if (isRunningSoundPlaying)
                {
                    runSoundSource.Stop();
                    isRunningSoundPlaying = false;
                }
                return;
            }
            if (!isGrounded)
            {
                if (isRunningSoundPlaying)
                {
                    runSoundSource.Stop();
                    isRunningSoundPlaying = false;
                }
                return;
            }
            if (!isRunningSoundPlaying && isGrounded)
            {
                runSoundSource.pitch = isSprinting ? baseRunSoundPitch * sprintPitchMultiplier : baseRunSoundPitch;
                runSoundSource.clip = runSound;
                runSoundSource.loop = true;
                runSoundSource.Play();
                isRunningSoundPlaying = true;
            }
            else if (isRunningSoundPlaying)
            {
                runSoundSource.pitch = isSprinting ? baseRunSoundPitch * sprintPitchMultiplier : baseRunSoundPitch;
            }
        }
        private bool IsBlockedByObstacle()
        {
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
            float rayDistance = 1.0f; 
            return Physics.Raycast(ray, rayDistance, obstacleLayers);
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01f)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
        }
    }
}
