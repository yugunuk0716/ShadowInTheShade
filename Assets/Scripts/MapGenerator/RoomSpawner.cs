using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int _openingDirection;
	// 1 --> 아래가 뚫린게 필요한놈
	// 2 --> 위가 뚫린게 필요한놈
	// 3 --> 왼쪽이 뚫린게 필요한놈
	// 4 --> 오른쪽이 뚫린게 필요한놈


	public RoomTemplates _templates;
	private int _rand;
	public bool _spawned = false;

	public float _waitTime = 4f;

	public Door _door;

	void Start(){
		//Destroy(gameObject, _waitTime);
		_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke(nameof(Spawn), 0.1f);
	}


	void Spawn(){
		if(_spawned == false){
			if(_openingDirection == 1){
				_rand = Random.Range(0, _templates._bottomRooms.Length);
				Instantiate(_templates._bottomRooms[_rand], transform.position, _templates._bottomRooms[_rand].transform.rotation);
			} else if(_openingDirection == 2){
				_rand = Random.Range(0, _templates._topRooms.Length);
				Instantiate(_templates._topRooms[_rand], transform.position, _templates._topRooms[_rand].transform.rotation);
			} else if(_openingDirection == 3){
				_rand = Random.Range(0, _templates._leftRooms.Length);
				Instantiate(_templates._leftRooms[_rand], transform.position, _templates._leftRooms[_rand].transform.rotation);
			} else if(_openingDirection == 4){
				_rand = Random.Range(0, _templates._rightRooms.Length);
				Instantiate(_templates._rightRooms[_rand], transform.position, _templates._rightRooms[_rand].transform.rotation);
			}
			_spawned = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint")){
			RoomSpawner rs = other.GetComponent<RoomSpawner>();
			if (rs._spawned == false && _spawned == false){

				if (_templates == null)
				{
					_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

                }
                
				Instantiate(_templates._closedRoom, transform.position, Quaternion.identity);
				//Destroy(gameObject);
			} 
			_spawned = true;
		}
     
        
    }
}
