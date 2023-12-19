using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{

    public float walkSpeed = 16f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 1f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    private bool facingRight = true; // Assuming this variable is used for flipping

    public bool canMove = true;

    public Collider[] attackHitboxes;
    // Update is called once per frame

    public float knockbackForce = 500f;

    public AudioSource audioPlayer;
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {


        #region Handles hit
        if(Input.GetKeyDown(KeyCode.G))
           LaunchAttack(attackHitboxes[0]);
        if(Input.GetKeyDown(KeyCode.H))
           LaunchAttack(attackHitboxes[1]);
        #endregion

        #region Handles Movment
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = right * curSpeedY;

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

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0 && canMove)
        {
        Vector3 moveDirection = transform.right * moveInput;
        characterController.Move(moveDirection * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * Time.deltaTime);

        if (moveInput < 0 && facingRight || moveInput > 0 && !facingRight)
        {
            Flip();
        }
        }
        #endregion

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        }
        
        void Flip()
        {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        }

        private void LaunchAttack (Collider col)
         {
             Collider[] cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
             foreach(Collider c in cols)
             {
                 if(c.transform.parent.parent == transform)
                 continue;
 
                 float damage = 0;
                 switch(c.name)
                 {
                     case "Head":
                     damage = 30;
                     break;
                     case "Torso":
                     damage = 10;
                     break;
                     default:
                     Debug.Log("Unable to identify body part");
                     break;
                 }
     
 
             c.SendMessageUpwards("TakeDamage",damage);
             audioPlayer.Play(); 

            Vector3 knockbackDirection = c.transform.position - col.transform.position;
            knockbackDirection.Normalize();
            c.transform.parent.parent.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
 
         }
    }
        
}