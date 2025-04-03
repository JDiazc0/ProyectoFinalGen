using UnityEngine;

public class ObjectMessage : MonoBehaviour
{
    public GameObject canvasMensaje;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto " + other.gameObject.name + " entró en el trigger.");
            canvasMensaje.SetActive(true);
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
