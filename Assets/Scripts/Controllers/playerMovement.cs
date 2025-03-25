using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    public Animator animator; // Referencia al Animator

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private Vector2 moveInput;

    private PlayerInput playerControls;

    void Start()
    {
        playerControls = GetComponent<PlayerInput>();

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("No se encontró CharacterController en el Player.");
            }
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError("No se encontró Animator en el Player.");
            }
        }
    }

    void Update()
    {
        if (characterController == null || playerControls == null) return;

        // --- OBTENER INPUT DE MOVIMIENTO ---
        moveInput = playerControls.actions["move"].ReadValue<Vector2>();

        bool isSprinting = Keyboard.current.leftShiftKey.isPressed; // Shift presionado
        bool isMoving = moveInput.sqrMagnitude > 0.01f; // Verifica si hay movimiento

        // --- CALCULAR VELOCIDAD ---
        float speed = isSprinting ? runSpeed : walkSpeed;
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (isMoving)
        {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

        // --- CONTROL DE ANIMACIONES ---
        if (animator != null)
        {
            animator.SetBool("isRunning", isMoving && isSprinting);
        }
    }
}
