using UnityEngine;
using DG.Tweening;

public class ButtonSlideLR_1 : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(-1200, 200, 0);
        ShowWindow();
    }

    void ShowWindow()
    {
        //　SetEase = アニメーション
        rectTransform.DOLocalMoveX(-750f, 0.8f).SetEase(Ease.OutSine);
    }
}
