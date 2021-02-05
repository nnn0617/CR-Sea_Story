using UnityEngine;
using DG.Tweening;

public class Jump : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StartNewRecordAnim();
    }

    void StartNewRecordAnim()
    {
        rectTransform.DOLocalMoveY(30f, 0.5f).SetRelative(true).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }
}