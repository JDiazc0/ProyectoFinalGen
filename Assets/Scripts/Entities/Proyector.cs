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
    private bool isPlaying = false;
    public DoubleDoorInteraction scriptAControlar;
    public GameObject llave;
    public GameObject llave2;
    public GameObject LlaveTerapia;

    void Start()
    {
        videoPlayer.Stop();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        mensajeText.gameObject.SetActive(false);
        llave.SetActive(false);
        llave2.SetActive(false);

        if (scriptAControlar != null)
        {
            scriptAControlar.enabled = false;
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaying)
        {
            if (other.CompareTag("escena1"))
            {
                ReproducirVideo(0);
            }
            else if (other.CompareTag("escena2"))
            {
                ReproducirVideo(1);

                GameObject player = GameObject.Find("Player");
                if (player != null)
                {
                    player.tag = "Terapia";
                }
                else
                {
                    Debug.LogWarning("No se encontró un objeto con el nombre 'Player'.");
                }
            }
            else if (other.CompareTag("escena3"))
            {
                ReproducirVideo(2);
                LlaveTerapia.SetActive(true);
            }
        }
    }


    private void ReproducirVideo(int index)
    {
        if (index < videos.Length && index < mensajes.Length)
        {
            videoPlayer.clip = videos[index];
            videoPlayer.Play();
            isPlaying = true;
            ActivarScript();
            StartCoroutine(MostrarMensajePorTiempo(mensajes[index], 5f));
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        isPlaying = false;
        OcultarMensaje();
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






