using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] float groundCheckRadius=0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;
    float ySpeed;
    private bool isJumping;

    CameraController cameraController;
    Quaternion targetRotation;
    Animator animator;
    CharacterController characterController;
    
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveInput = (new Vector3(h, 0, v)).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;
        GroundCheck();
        if (isGrounded && animator.GetBool("isFalling")){
            animator.SetBool("isFalling", false);
        }
        if (isGrounded){
            animator.SetBool("isGrounded", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump")){
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
            }
        } else {
            animator.SetBool("isGrounded", false);
            ySpeed += Physics.gravity.y * Time.deltaTime;
            if (ySpeed < 0){
                animator.SetBool("isFalling", true);
            }
            if (moveAmount > 0){
                targetRotation = Quaternion.LookRotation(moveDir);
            }
        }
        var velocity = moveDir*moveSpeed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
        if (moveAmount >0){
            
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }
    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
