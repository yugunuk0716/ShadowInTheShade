using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour 
{

	public int _openingDirection;
	// 1 --> 아래가 뚫린게 필요한놈
	// 2 --> 위가 뚫린게 필요한놈
	// 3 --> 왼쪽이 뚫린게 필요한놈
	// 4 --> 오른쪽이 뚫린게 필요한놈


	private int _rand;
	public bool _spawned = false;

	public float _waitTime = 4f;

	public Door _door;

	void Start(){
		//Destroy(gameObject, _waitTime);
		Invoke(nameof(Spawn), 0.1f);
	}


	void Spawn(){
		if(_spawned == false){
			//if(_openingDirection == 1){
			//	_rand = Random.Range(0, RoomTemplates.Instance._bottomRooms.Length);
			//	Instantiate(RoomTemplates.Instance._bottomRooms[_rand], transform.position, RoomTemplates.Instance._bottomRooms[_rand].transform.rotation);
			//} else if(_openingDirection == 2){
			//	_rand = Random.Range(0, RoomTemplates.Instance._topRooms.Length);
			//	Instantiate(RoomTemplates.Instance._topRooms[_rand], transform.position, RoomTemplates.Instance._topRooms[_rand].transform.rotation);
			//} else if(_openingDirection == 3){
			//	_rand = Random.Range(0, RoomTemplates.Instance._leftRooms.Length);
			//	Instantiate(RoomTemplates.Instance._leftRooms[_rand], transform.position, RoomTemplates.Instance._leftRooms[_rand].transform.rotation);
			//} else if(_openingDirection == 4){
			//	_rand = Random.Range(0, RoomTemplates.Instance._rightRooms.Length);
			//	Instantiate(RoomTemplates.Instance._rightRooms[_rand], transform.position, RoomTemplates.Instance._rightRooms[_rand].transform.rotation);
			//}
			PoolManager.Instance.CreateStage(_openingDirection, this.transform.position);
			_spawned = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint")){
			RoomSpawner rs = other.GetComponent<RoomSpawner>();
			if (rs._spawned == false && _spawned == false){

				//Instantiate(RoomTemplates.Instance._closedRoom, transform.position, Quaternion.identity);
				//Destroy(gameObject);
			} 
			_spawned = true;
		}
     
        
    }


}
