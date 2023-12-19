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

    private bool facingRight = true; // Assuming this variable is used for flipping

    private Vector3 moveDirection = Vector3.zero;
    //private float rotationX = 0;

    public bool canMove = true;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

void Update()
{
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

    // Process Jumping
    if (characterController.isGrounded && Input.GetButtonDown("Jump") && canMove)
    {
        moveDirection.y = jumpPower;
    }

    moveDirection.y -= gravity * Time.deltaTime;
    characterController.Move(moveDirection * Time.deltaTime);

    void Flip()
{
    facingRight = !facingRight;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1;
    transform.localScale = localScale;
}

}

}
