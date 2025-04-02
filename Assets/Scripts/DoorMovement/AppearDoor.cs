using UnityEngine;

public class AppearDoor : MonoBehaviour
{
    public DoubleDoorInteraction scriptAControlar; // Arrastra aquí el objeto con MiScript

    void Start()
    {
        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = false; // Desactiva el script
        }
    }

    public void ActivarScript()
    {
        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = true; // Activa el script
            Debug.Log("¡MiScript ha sido activado!");
        }
    }
}