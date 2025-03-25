using UnityEngine;
using UnityEngine.InputSystem;

public class FPPController1 : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerInput playerControls;
    public Transform cameraTransform;
    private CharacterController characterController;

    [Header("Estadísticas del personaje")]
    [SerializeField] private float walkSpeed = 1.9f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float jumpHeight = 10f;

    [Header("Variables del Jugador")]
    [SerializeField] private float speed;
    [SerializeField] private bool isGrounded;
    private Vector3 velocity;

    [Header("Estadísticas del mundo")]
    public float gravity = -30f;

    [Header("Variable de colisión")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask ground;
    public float raycastLength = 0.5f;

    [Header("Agacharse")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    private bool isCrouching = false;
    public Transform headCheck;  // Punto para verificar colisiones al levantarse
    public float headCheckDistance = 1.2f;  // Distancia para el Raycast

    void Start()
    {
        playerControls = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move();
        Jump();
        HandleCrouch();

        // Aplicar gravedad siempre
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 _moveHV = playerControls.actions["Move"].ReadValue<Vector2>();
        Vector3 move = cameraTransform.right * _moveHV.x + cameraTransform.forward * _moveHV.y;

        move.y = 0f;
        move.Normalize();

        // Ajustar velocidad según estado (corriendo, caminando, agachado)
        speed = isCrouching ? crouchSpeed : (playerControls.actions["Run"].IsInProgress() ? runSpeed : walkSpeed);

        characterController.Move(move * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (playerControls.actions["Jump"].WasPressedThisFrame() && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * -gravity);
        }
    }

    private void HandleCrouch()
    {
        if (playerControls.actions["Crouch"].WasPressedThisFrame())
        {
            if (!isCrouching)
            {
                // Agacharse
                characterController.height = crouchingHeight;
                isCrouching = true;
            }
            else
            {
                // Verificar si hay espacio para ponerse de pie
                if (!Physics.Raycast(headCheck.position, Vector3.up, headCheckDistance))
                {
                    characterController.height = standingHeight;
                    isCrouching = false;
                }
            }
        }
    }
}
|||||||||||||||||||||||