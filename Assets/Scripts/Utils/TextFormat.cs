using TMPro;
using UnityEngine;

public class TextFormat : MonoBehaviour
{

    public TextMeshProUGUI creditsText;

    private void Start()
    {
        string formattedCredits =
            "<align=center><b><size=120%>CRÉDITOS</size></b></align>\n\n" +

            "<b><color=#FFA500>Game Programmers</color></b>\n" +
            "Antheo Mauricio Patarroyo\n" +
            "Camilo Varga Cortes\n" +
            "Carlos Botina\n" +
            "Jhoan Andres Diaz\n\n" +

            "<b><color=#FFA500>Game Testers</color></b>\n" +
            "Antheo Mauricio Patarroyo\n" +
            "Camilo Varga Cortes\n" +
            "Carlos Botina\n" +
            "Jhoan Andres Diaz\n\n" +

            "<b><color=#FFA500>Game Level Designers</color></b>\n" +
            "Antheo Mauricio Patarroyo\n" +
            "Camilo Varga Cortes\n" +
            "Carlos Botina\n" +
            "Jhoan Andres Diaz\n\n" +

            "<b><color=#FFA500>UX/UI Programmer</color></b>\n" +
            "Antheo Mauricio Patarroyo\n" +
            "Camilo Varga Cortes\n" +
            "Carlos Botina\n" +
            "Jhoan Andres Diaz\n\n" +

            "<b><color=#FFA500>Assets</color></b>\n" +
            "Abandoned Asylum - Lukas Bobor\n\n" +

            "<b><color=#FFA500>Música y Sonidos</color></b>\n" +
            "Free Horror Ambience 2 - N91music\n" +
            "Horror Background Atmosphere #6 - Universfield";

        creditsText.text = formattedCredits;

    }
}
