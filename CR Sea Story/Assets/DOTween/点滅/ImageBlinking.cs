using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    // 点滅対象
    public Image Limited;

    // アニメーション開始のトリガー
    public Button StartButton;

    private const float AnimeTimeSec = 1.0f;
    private const int AnimeRepeatCount = 3;
    private int count;
    private float defaultAlpha;
    private Vector3 defaultScale;
    private Vector3 largeScale;

    private void Start()
    {
        defaultAlpha = Limited.color.a;
        defaultScale = Limited.rectTransform.localScale;

        largeScale = new Vector3(
            defaultScale.x * 1.2f,
            defaultScale.y * 1.2f,
            defaultScale.z * 1.2f);

        StartButton.onClick.AddListener(() =>
        {
            // 実行中のアニメーションをリセット
            count = 0;
            DOTween.Clear();
            // 点滅アニメーション
            StartBlinking();
        });
    }

    private void StartBlinking()
    {
        var startColor = Limited.color;
        startColor.a = 0f;
        Limited.color = startColor;

        Limited.rectTransform.localScale = defaultScale;

        // largeScale = アニメーション完了後のScale 
        // AnimeTimeSec = アニメーションの実行時間
        Limited.rectTransform.DOScale(largeScale, AnimeTimeSec).SetEase(Ease.Linear);

        //() => Limited.color = Alpha値変更対象のColor(Getter) 
        // value => Limited.color = value =  Setter
        // defaultAlpha = アニメーション完了後のAlpha 
        // AnimeTimeSec = アニメーションの実行時間
        DOTween.ToAlpha(() => Limited.color, value => Limited.color = value, defaultAlpha, AnimeTimeSec)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                count += 1;
                if (count < AnimeRepeatCount)
                {
                    // 一定回数繰り返し
                    StartBlinking();
                }
                else
                {
                    Debug.Log("finished");
                }
            });
    }
}
