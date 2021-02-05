using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReTitle : MonoBehaviour
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
            ButtonSE.PlayOneShot(ButtonSE.clip);
            isCalledOnce = true;
            FadeManager.FadeOut(0);
            Debug.Log("Titleへ");
        }
    }
}
