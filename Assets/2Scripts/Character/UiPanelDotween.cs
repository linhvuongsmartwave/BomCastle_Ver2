using UnityEngine;
using DG.Tweening;

public class UiPanelDotween : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    public void PanelFadeIn()
    {
        AudioManager.Instance.AudioOpen();
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0, -700f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
    }

    public void PanelFadeOut()
    {
        AudioManager.Instance.AudioButtonClick();
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, -2000f), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
    }
}
