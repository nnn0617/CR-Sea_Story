using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonChoice_Stage : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        var trigger = button.GetComponent<EventTrigger>();

        //マウスカーソルが触れたとき
        EventTrigger.Entry pointEnter = new EventTrigger.Entry();
        pointEnter.eventID = EventTriggerType.PointerEnter;
        pointEnter.callback.AddListener((data) =>
        {
            transform.DOScale(5.5f, 0.5f).SetEase(Ease.OutElastic);
            Debug.Log("選択中");
        });

        //マウスカーソルが離れたとき
        EventTrigger.Entry pointExit = new EventTrigger.Entry();
        pointExit.eventID = EventTriggerType.PointerExit;
        pointExit.callback.AddListener((data) =>
        {
            transform.DOScale(5.0f, 0.5f).SetEase(Ease.OutElastic);
            Debug.Log("選択解除");
        });

        //EventTriggerを登録
        trigger.triggers.Add(pointEnter);
        trigger.triggers.Add(pointExit);
    }
}
