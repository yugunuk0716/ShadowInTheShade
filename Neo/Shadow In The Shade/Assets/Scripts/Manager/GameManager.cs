using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance  
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                obj.AddComponent<GameManager>();
                instance = obj.GetComponent<GameManager>();
            }

            return instance;
        }
    }

    public Transform player;


    [Range(0f, 1f)]
    public float timeScale = 1f;

    public UnityEvent onPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent onPlayerAttack; //�÷��̾ ������ �� ���� �̺�Ʈ
    public UnityEvent onPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ

    public PlayerSO playerSO;


    private void Awake()
    {
        init();
    }


    public void init()
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Human;
        playerSO.canChangePlayerType = true;
    }

}
