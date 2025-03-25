using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miPropioControladorFPS : MonoBehaviour
{
    Rigidbody rb;
    Vector2 inputMov;
    Vector2 inputRot;
    public float velCamina = 10f;
    public float velCorre = 20f;
    public float fuerzaSalto = 300;

    public float sensibilidadMouse = 500;
    Transform cam;
    float rotX;

    Vector3 escalaNormal;
    Vector3 escalaAgachado;
    bool agachado;

    void Start()
    {
        Cursor.visible = false; //ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked; //lo bloquea
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0);
        rotX = cam.eulerAngles.x;

        escalaNormal = transform.localScale;
        escalaAgachado = escalaNormal;
        escalaAgachado.y = escalaNormal.y * .75f;
    }

    void Update()
    {
        //leemos el input
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        inputRot.x = Input.GetAxis("Mouse X") * sensibilidadMouse;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        agachado = Input.GetKey(KeyCode.C);

        //salto
        if (Input.GetButtonDown("Jump") && EnElSuelo())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(fuerzaSalto * 2f * Physics.gravity.magnitude), rb.linearVelocity.z);
        }


    }

    private void FixedUpdate()
    {
        //usamos ese input para movernos y girar
        float vel = Input.GetKey(KeyCode.LeftShift) ? velCorre : velCamina;


        rb.linearVelocity =
        transform.forward * vel * inputMov.y   //movernos hacia atras y delante
       + transform.right * vel * inputMov.x    //deslizarnos hacia los costados
       + new Vector3(0, rb.linearVelocity.y, 0)

       ;

        transform.rotation *= Quaternion.Euler(0, inputRot.x, 0);   //rotar horizontalmente

        //mirar hacia arriva y hacia abajo
        rotX -= inputRot.y;
        rotX = Mathf.Clamp(rotX, -50, 50);
        cam.localRotation = Quaternion.Euler(rotX, 0, 0);


        //agacharse o erguirse
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            agachado ? escalaAgachado : escalaNormal,
            .10f);


    }
    bool EnElSuelo()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }


}
