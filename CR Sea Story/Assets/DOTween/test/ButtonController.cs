using UnityEngine;
using DG.Tweening;

public class ButtonController : MonoBehaviour
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



//using UnityEngine;
//using DG.Tweening;

//public class ButtonController : MonoBehaviour
//{
//    void Awake()
//    {
//        transform.localScale = Vector3.zero;
//        ShowWindow();
//    }

//    void ShowWindow()
//    {
//        transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
//    }
//}
