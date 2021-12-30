using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    Pool<Stage> _stagePrefab;
	public GameObject _prefab;


    private void Start()
    {
		_stagePrefab = new Pool<Stage>(new PrefabFactory<Stage>(_prefab), 5);

	}

    public void CreateText()
	{
		Stage stage = _stagePrefab.Allocate();

		EventHandler handler = null;
		handler = (sender, e) => {
			_stagePrefab.Release(stage);
			stage.Death -= handler;
		};

		stage.Death += handler;
		stage.gameObject.SetActive(true);
	}
}
