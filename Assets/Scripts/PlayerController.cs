using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    //Movement declaration

    //Movement
    public float walkSpeed = 16f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 1f;

    //Rotate
    Vector3 moveDirection = Vector3.zero;
    private bool facingRight = true;

    public bool canMove = true;

    //Crouch
    bool isCrouching = false;
    float originalHeight;
    private float crouchHeight = 0.5f;

    //Dash
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;


    [SerializeField] private Rigidbody rb;

    //end declaration

    //Action declaration
    public Collider[] attackHitboxes;
    public float knockbackForce = 500f;
    public AudioSource audioPlayer;
    //End declaration

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalHeight = characterController.height; // Simpan tinggi karakter awal
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!PauseMenu.isPaused)
        {
        #region Handles hit
        if(Input.GetKeyDown(KeyCode.G))
           LaunchAttack(attackHitboxes[0]);
        if(Input.GetKeyDown(KeyCode.H))
           LaunchAttack(attackHitboxes[1]);
        #endregion

        #region Handles Dash
            if (Input.GetKeyDown(KeyCode.F) && canMove && !isDashing)
            {
                float dashDirection = Input.GetAxisRaw("Horizontal"); // Get horizontal input

                if (dashDirection != 0)
                {
                    StartCoroutine(Dash(dashDirection));
                }
            }
            #endregion


        #region Handles Movement
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

        #region Handles Crouch
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                characterController.height = crouchHeight; // Kurangi tinggi karakter saat crouch
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (isCrouching)
            {
                isCrouching = false;
                characterController.height = originalHeight; // Kembalikan tinggi karakter ke nilai semula
            }
        }
    #endregion

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        }
        }
        
        void Flip()
        {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        }

      IEnumerator Dash(float direction)
      {
        isDashing = true;
        float startTime = Time.time;
        Vector3 originalPosition = transform.position;
        Vector3 dashEndPosition = originalPosition + Vector3.right * direction * dashDistance; // Change the direction of the dash

        while (Time.time < startTime + dashDuration)
        {
            float percentage = (Time.time - startTime) / dashDuration;
            characterController.Move(Vector3.right * direction * dashDistance * Time.deltaTime / dashDuration);
            yield return null;
        }

        isDashing = false;
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
            // c.transform.parent.parent.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            if (facingRight)
            {

            c.transform.parent.parent.GetComponent<Rigidbody>().AddForce(transform.right * 5, ForceMode.Impulse);
 
            } else {
            c.transform.parent.parent.GetComponent<Rigidbody>().AddForce(transform.right * -5, ForceMode.Impulse);
            }
            }
        }

        
}