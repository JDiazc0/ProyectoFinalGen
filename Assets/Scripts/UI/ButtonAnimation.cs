using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image gradientImage;
    public TMP_Text buttonText;
    private Color textHoverColor = Color.black;
    private Color textNormalColor;

    void Start()
    {
        if (gradientImage != null)
        {
            gradientImage.fillAmount = 0;
        }

        if (buttonText != null)
        {
            textNormalColor = buttonText.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.value(gameObject, 0, 1, 0.5f)
            .setOnUpdate((float value) =>
            {
                if (gradientImage != null)
                {
                    gradientImage.fillAmount = value;
                }
            }).setEase(LeanTweenType.easeOutQuad);

        LeanTween.value(gameObject, buttonText.color, textHoverColor, 0.5f)
            .setOnUpdate((Color color) =>
            {
                buttonText.color = color;
            }).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.value(gameObject, 1, 0, 0.5f)
            .setOnUpdate((float value) =>
            {
                if (gradientImage != null)
                {
                    gradientImage.fillAmount = value;
                }
            })
            .setEase(LeanTweenType.easeOutQuad);
        LeanTween.value(gameObject, buttonText.color, textNormalColor, 0.5f)
            .setOnUpdate((Color color) =>
            {
                buttonText.color = color;
            }).setEase(LeanTweenType.easeOutQuad);
    }
}