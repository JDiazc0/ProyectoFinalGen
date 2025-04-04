using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.Collections;

public class Proyector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public TextMeshProUGUI mensajeText;
    public VideoClip[] videos;
    public string[] mensajes;
    private bool hasPlayed = false;
    public DoubleDoorInteraction scriptAControlar;

    void Start()
    {
        videoPlayer.Stop();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        mensajeText.gameObject.SetActive(false);

        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed)
        {
            if (other.CompareTag("escena1"))
            {
                ReproducirVideo(0);
            }
            else if (other.CompareTag("escena2"))
            {
                ReproducirVideo(1);
            }
            else if (other.CompareTag("escena3"))
            {
                ReproducirVideo(2);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("escena1") || other.CompareTag("escena2") || other.CompareTag("escena3"))
        {
            videoPlayer.Stop();
            hasPlayed = false;
            OcultarMensaje();
        }
    }

    private void ReproducirVideo(int index)
    {
        if (index < videos.Length && index < mensajes.Length)
        {
            videoPlayer.clip = videos[index];
            videoPlayer.Play();
            hasPlayed = true;
            ActivarScript();
            StartCoroutine(MostrarMensajePorTiempo(mensajes[index], 3f));
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

    private IEnumerator MostrarMensajePorTiempo(string mensaje, float tiempo)
    {
        if (mensajeText != null)
        {
            mensajeText.text = mensaje;
            mensajeText.gameObject.SetActive(true);
            yield return new WaitForSeconds(tiempo);
            OcultarMensaje();
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





