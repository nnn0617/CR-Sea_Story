using UnityEngine;
using DG.Tweening;

public class ButtonSlideLR_3 : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(-1200, -400, 0);
        ShowWindow();
    }

    void ShowWindow()
    {
        //　SetEase = アニメーション
        rectTransform.DOLocalMoveX(-750f, 1.2f).SetEase(Ease.OutSine);
    }
}
