using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Proyector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public TextMeshProUGUI mensajeText; // Referencia al TextMeshPro
    private bool isNearProjector = false;
    private bool hasPlayed = false;
    public string grabbableLayerName = "Grabbable";
    public DoubleDoorInteraction scriptAControlar;

    void Start()
    {
        videoPlayer.Stop();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        mensajeText.gameObject.SetActive(false); // Oculta el mensaje al inicio

        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = false;
        }
    }

    void Update()
    {
        if (isNearProjector && !hasPlayed)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                hasPlayed = true;
                ActivarScript();
                MostrarMensaje("En este lugar, el aroma es fuerte,donde el líquido oscuro es reconfortante.La gente aquí viene a charlar o estudiar,y muchos se sientan con tazas a esperar.¿Qué lugar es?");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(grabbableLayerName))
        {
            isNearProjector = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(grabbableLayerName))
        {
            isNearProjector = false;
            videoPlayer.Stop(); // Detener el video cuando el objeto se aleje
            hasPlayed = false; // Permite volver a reproducir el video si el objeto se acerca de nuevo
            OcultarMensaje();
        }
    }

    public void ActivarScript()
    {
        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = true;
            Debug.Log("¡MiScript ha sido activado!");
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        if (mensajeText != null)
        {
            mensajeText.text = mensaje;
            mensajeText.gameObject.SetActive(true);
        }
    }

    private void OcultarMensaje()
    {
        if (mensajeText != null)
        {
            mensajeText.gameObject.SetActive(false);
        }
    }
}




