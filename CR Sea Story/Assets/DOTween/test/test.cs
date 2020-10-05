using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class test : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        transform.DOScale(3.3f, 0.5f).SetEase(Ease.OutElastic);
        GetComponentInChildren<Text>().text = "押された";
    }
}
