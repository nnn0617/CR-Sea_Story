using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    [SerializeField] private AudioSource ButtonSE;
    void Start()
    {
        FadeManager.FadeIn();
    }
    bool isCalledOnce = false;


    void Update()
    {
        if (!isCalledOnce)
        {
            if (Input.anyKey)
            {
                isCalledOnce = true;
                //SceneManager.LoadScene("MenuScene");
                FadeManager.FadeOut(2);
                Debug.Log("StageSelectへ");
            }
        }
    }
}
