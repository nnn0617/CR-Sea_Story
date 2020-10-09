using UnityEngine;
using DG.Tweening;

public class ButtonSlideLR_2 : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(-1200, -100, 0);
        ShowWindow();
    }

    void ShowWindow()
    {
        //　SetEase = アニメーション
        rectTransform.DOLocalMoveX(-750f, 1f).SetEase(Ease.OutSine);
    }
}
