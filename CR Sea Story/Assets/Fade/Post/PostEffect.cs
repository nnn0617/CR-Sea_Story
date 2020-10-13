using UnityEngine;
using System.Collections;

public class PostEffect : MonoBehaviour
{

	public Material wipeCircle;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, wipeCircle);
	}
}
