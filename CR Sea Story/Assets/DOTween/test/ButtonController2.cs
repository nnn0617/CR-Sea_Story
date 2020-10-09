using UnityEngine;
using DG.Tweening;

public class ButtonController2 : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(300, 700, 0);
        ShowWindow();
    }

    void ShowWindow()
    {
        rectTransform.DOLocalMoveY(0f, 2f).SetEase(Ease.OutBounce);
    }
}
