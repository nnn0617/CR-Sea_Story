using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ExpansionUI : MonoBehaviour
{

    public GameObject panel;
    private bool isDefaultScale;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isDefaultScale)
            {
                panel.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
                isDefaultScale = false;
            }
            else if (!isDefaultScale)
            {
                panel.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
                isDefaultScale = true;
            }
        }
    }
}