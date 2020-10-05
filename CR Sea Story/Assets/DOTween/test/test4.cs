using UnityEngine;
using DG.Tweening;

public class test4 : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        //rectTransform = GetComponent();
        StartNewRecordAnim();
    }

    void StartNewRecordAnim()
    {
        rectTransform.DOLocalMoveY(20f, 0.4f)
        .SetRelative(true)
        .SetEase(Ease.OutQuad)
        .SetLoops(-1, LoopType.Yoyo);
    }
}