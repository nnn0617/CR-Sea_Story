using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
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
                FadeManager.FadeOut(1);
                Debug.Log("Menuへ");
            }
        }
    }
}
