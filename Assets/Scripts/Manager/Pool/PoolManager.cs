using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
	const int START_SIZE = 5;

	public List<Pool<Stage>> _stagePrefabList;
	public List<GameObject> _bottomMap;
	public List<GameObject> _topMap;
	public List<GameObject> _rightMap;
	public List<GameObject> _leftMap;





    public PoolManager()
    {


    }

    //private void Awake()
    //{
        
    //    for (int i = 0; i < _stagePrefabs.Count; i++)
    //    {
    //        Pool<Stage> pool = new Pool<Stage>(new PrefabFactory<Stage>(_stagePrefabs[i]), START_SIZE);
           
    //        _stagePrefabList.Add(pool);
    //    }

       
    //}

 //   public void CreateStage()
	//{
	//	int index = 0;
	//	do
	//	{
	//		index = UnityEngine.Random.Range(0, _stagePrefabs.Count - 1);

	//	} while (index == _fastIndex);

 //       if (_stageNumber == 0)// 맨 처음 맵이라면 장애물이 아무것도 없는 맵 템플릿을 소환
 //           index = _stagePrefabs.Count - 1;//_stagePrefabs의 마지막 오브젝트는 빈 맵으로 해야댐

	//	_fastIndex = index;
 //       _stageNumber++;

 //       Stage stage = _stagePrefabList[index].Allocate();

        

 //       EventHandler handler = null;
 //       handler = (sender, e) =>
 //       {
 //           _stagePrefabList[index].Release(stage);
 //           stage.Death -= handler;
 //       };

 //       stage.Death += handler;
 //       stage.gameObject.SetActive(true);


 //   }
}
