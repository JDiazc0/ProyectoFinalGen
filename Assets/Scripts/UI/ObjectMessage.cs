using UnityEngine;

public class ObjectMessage : MonoBehaviour
{
    public GameObject canvasMensaje;
    public GameObject activatePortal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto " + other.gameObject.name + " entró en el trigger.");
            canvasMensaje.SetActive(true);
        }
        if (other.CompareTag("Mug"))
        {
            activatePortal.SetActive(true);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto " + other.gameObject.name + " salió del trigger.");
            canvasMensaje.SetActive(false);
        }
    }
}
