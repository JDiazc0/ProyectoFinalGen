using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; // Referencia a la luz de la linterna
    private bool isOn = false; // Estado inicial apagado

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>(); // Buscar la luz automáticamente
        }
        flashlight.enabled = isOn; // Asegurar que inicie apagada
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Detectar la tecla "F"
        {
            isOn = !isOn; // Cambiar el estado
            flashlight.enabled = isOn; // Activar o desactivar la luz
        }
    }
}
