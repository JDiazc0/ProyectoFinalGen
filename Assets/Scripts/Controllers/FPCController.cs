    using UnityEngine;
using UnityEngine.InputSystem;

public class FPCController : MonoBehaviour
{
    [Header("References")]

    public PlayerInput playerControlls;

    public Transform fPCPosition;
   public GameObject  FPbody;
    public Vector3 fixedFirstPersonArmsPosition;
    [Header("Camera Settings")]
    public float mouseSensibility = 10f;
    private float yRotation = 0f;
    private float xRotation = 0f;
    public Animator FpAnimator ;
    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = playerControlls.actions["look"].ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensibility * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensibility * Time.deltaTime;

        //acumulacion de rotacion
        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);

        transform.position = fPCPosition.position;
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
        FPbody.transform.Rotate(Vector3.up*mouseX);

        //ajustar posicion de brazos
        FPbody.transform.localPosition = fixedFirstPersonArmsPosition;
        FPbody.transform.localRotation = Quaternion.identity;

    }

         

    }

