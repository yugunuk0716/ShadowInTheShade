using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoSingleton<EffectManager>
{
	float a = 1;
	public Image image;

	//Cinemachine Camera
	public GameObject _cinemachineCamObj;
	[HideInInspector]
	public CinemachineConfiner _cinemachineCamConfiner;
	[HideInInspector]
	public CinemachineVirtualCamera _cinemachineCam;

	// Start is called before the first frame update
	void Start()
	{
		_cinemachineCamConfiner = _cinemachineCamObj.GetComponent<CinemachineConfiner>();
		_cinemachineCam = _cinemachineCamObj.GetComponent<CinemachineVirtualCamera>();

		_cinemachineCamConfiner.m_BoundingShape2D = StageManager.Instance._rooms.Find((r) => r._isEntry)._camBound;
		StartCoroutine(FadeOut());

	}

	
	public void StartFadeIn()
    {
		StartCoroutine(FadeIn());
    }

	public void StartFadeOut()
	{
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeIn()
	{
		print("?");
		while (true)
		{
			a += 0.01f;
			print(a);
			image.color = new Color(0, 0, 0, a);
			yield return new WaitForSeconds(0.01f);
			if (a >= 1)
				break;
		}


		StartCoroutine(FadeOut());
	}
	private IEnumerator FadeOut()
	{
		a = 1f;
		while (true)
		{
			a -= 0.01f;
			print(a);
			image.color = new Color(0, 0, 0, a);
			yield return new WaitForSeconds(0.01f);
			if (a <= 0)
				break;
		}

	}
}
