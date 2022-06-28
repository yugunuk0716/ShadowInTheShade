using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowChanger : MonoBehaviour
{
    public bool isS;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.enabled = !isS ? GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow): GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human);
        GameManager.Instance.onPlayerChangeType.AddListener(() => 
        {
            sr.enabled = !isS ? GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow): GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
