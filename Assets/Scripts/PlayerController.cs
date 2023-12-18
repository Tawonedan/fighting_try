using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    // public Camera playerCamera;
    public float walkSpeed = 16f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 1f;
 
 
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
 
    //
    Vector3 moveDirection = Vector3.zero;
    // float rotationX = 0;
 
    public bool canMove = true;
 
    
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void Update()
    {
 
        #region Handles Movment
        // Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
 
        // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = right * curSpeedY;
        // moveDirection = (forward * curSpeedX) + (right * curSpeedY);
 
        // press E to dash
        // if (Input.GetKeyDown(KeyCode.LeftShift))
        #endregion
 
        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        #endregion
 
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 

 
        #endregion
    }

}
 