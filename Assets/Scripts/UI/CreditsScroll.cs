using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform content;
    public RectTransform viewport;
    public float scrollSpeed = 50f;
    private float _contentHeight;
    private float _viewportHeight;
    private float _startY;

    private void Start()
    {
        _contentHeight = content.rect.height;
        _viewportHeight = viewport.rect.height;
        _startY = content.anchoredPosition.y;

        StartScrolling();
    }

    public void StartScrolling()
    {
        float _endY = _startY + (_contentHeight - _viewportHeight);

        LeanTween.moveY(content, _endY, (_contentHeight / scrollSpeed))
            .setEase(LeanTweenType.linear)
            .setOnComplete(() =>
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, _startY);
                StartScrolling();
            });
    }
}
