using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BBB : MonoBehaviour
{
    RectTransform rectTransform;
    Sequence seq = DOTween.Sequence();

    void Awake()
    {
        transform.localScale = Vector3.zero;
        ShowWindow();
    }

    public void ShowWindow()
    {
        //　SetEase = アニメーション
        seq.Append(transform.DOScale(5f, 1f).SetEase(Ease.OutBounce));
    }
}
