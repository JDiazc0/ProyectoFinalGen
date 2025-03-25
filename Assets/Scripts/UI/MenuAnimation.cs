// MenuAnimation.cs (en la carpeta UI)
using TMPro;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public RectTransform titlePanel;
    private TMP_Text textTitle;
    public RectTransform mainPanel;
    public RectTransform settingsPanel;
    public RectTransform creditsPanel;
    private bool isAnimationEnded = false;

    void Start()
    {
        textTitle = titlePanel.GetComponentInChildren<TMP_Text>();
        if (textTitle != null)
        {
            Color textColor = textTitle.color;
            textColor.a = 0;
            textTitle.color = textColor;
        }

        mainPanel.anchoredPosition = new Vector2(0, -Screen.height);
        settingsPanel.anchoredPosition = new Vector2(0, -Screen.height);
        creditsPanel.anchoredPosition = new Vector2(0, -Screen.height);

        settingsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);

        StartAnimations();
    }

    void Update()
    {
        if ((Input.anyKeyDown || Input.GetMouseButtonDown(0)) && !isAnimationEnded)
        {
            //SkipAnimations();
        }
    }

    void StartAnimations()
    {
        LeanTween.value(textTitle.gameObject, 0f, 1f, 3.5f).setEase(LeanTweenType.easeInOutQuad)
        .setOnUpdate((float val) =>
        {
            Color c = textTitle.color;
            c.a = val;
            textTitle.color = c;
        })
        .setOnComplete(() =>
        {
            LeanTween.moveY(titlePanel, 300, 1.5f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.scale(titlePanel, new Vector3(0.8f, 0.8f, 0.8f), 1.5f).setEase(LeanTweenType.easeOutBack)
                .setOnComplete(() =>
                {
                    mainPanel.gameObject.SetActive(true);
                    LeanTween.moveY(mainPanel, 0, 1f).setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(() => isAnimationEnded = true);
                });
        });

    }

    void SkipAnimations()
    {
        LeanTween.cancel(titlePanel.gameObject);
        LeanTween.cancel(mainPanel.gameObject);

        if (textTitle != null)
        {
            Color c = textTitle.color;
            c.a = 1f;
            textTitle.color = c;
        }

        titlePanel.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        titlePanel.anchoredPosition = new Vector2(titlePanel.anchoredPosition.x, 350);

        mainPanel.gameObject.SetActive(true);
        mainPanel.anchoredPosition = Vector2.zero;

        settingsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
        isAnimationEnded = true;
    }

    public void ShowMainPanel()
    {
        ShowPanel(mainPanel);
    }

    public void ShowSettingsPanel()
    {
        ShowPanel(settingsPanel);
    }

    public void ShowCreditsPanel()
    {
        ShowPanel(creditsPanel);
    }

    void ShowPanel(RectTransform panel)
    {
        HidePanel(mainPanel);
        HidePanel(settingsPanel);
        HidePanel(creditsPanel);

        panel.gameObject.SetActive(true);
        panel.anchoredPosition = new Vector2(0, -Screen.height);
        LeanTween.moveY(panel, 0, 1f).setEase(LeanTweenType.easeInOutQuad);
    }

    void HidePanel(RectTransform panelToHide)
    {
        if (panelToHide.gameObject.activeSelf && panelToHide.anchoredPosition.y == 0)
        {
            LeanTween.moveY(panelToHide, -Screen.height, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    panelToHide.gameObject.SetActive(false);
                });
        }
    }
}