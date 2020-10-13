using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DUO_MenuScene : MonoBehaviour
{
    [SerializeField] private AudioSource ButtonSE;
    void Start()
    {
        FadeManager.FadeIn();
    }
    bool isCalledOnce = false;

    public void OnClickStartButton()
    {
        if (!isCalledOnce)
        {
            isCalledOnce = true;
            FadeManager.FadeOut(3);
            Debug.Log("DUO_StageSelectへ");
        }
    }
}
