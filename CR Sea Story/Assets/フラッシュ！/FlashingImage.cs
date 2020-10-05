using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingImage : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 0.5f)] private float duration; // 継続時間
    [SerializeField] [Range(0.03f, 0.1f)] private float span;    // 点滅間隔
    [SerializeField] [Range(0f, 1f)] private float alphaMax;     // 不透明度

    [SerializeField] private Image image;            // Imageコンポーネント

    private void Flashing()
    {
        StartCoroutine(DoFlashing(duration, span, alphaMax));
    }

    private IEnumerator DoFlashing(float duration, float span, float alphaMax)
    {
        var elapsed = 0f;
        var spanCount = 0f;
        var isColored = false;
        var alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            spanCount += Time.deltaTime;

            if (spanCount > span)
            {
                alpha = isColored ? 0f : alphaMax;  // 不透明なら透明にする。透明なら不透明にする

                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                isColored = !isColored;

                spanCount = 0f;
            }

            yield return null;
        }

        // 透明に戻す
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
}
