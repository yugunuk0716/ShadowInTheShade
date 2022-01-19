using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoSingleton<EffectManager>
{
	float a = 1;
	public Image image;
	private void Start()
	{
		StartCoroutine(FadeOut());
	}


	public IEnumerator FadeIn()
	{
		print("?");
		while (true)
		{
			a += 0.01f;
			image.color = new Color(0, 0, 0, a);
			yield return new WaitForSeconds(0.01f);
			if (a >= 1)
				break;
		}


		StartCoroutine(FadeOut());
	}
	public IEnumerator FadeOut()
	{
		a = 1f;
		while (true)
		{
			a -= 0.005f;
			image.color = new Color(0, 0, 0, a);
			yield return new WaitForSeconds(0.01f);
			if (a <= 0)
				break;
		}

	}
}
