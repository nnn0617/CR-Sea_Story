using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SORO_GameScene : MonoBehaviour
{
    private GameObject _playerObj;
    private PlayerBehaviour _playerBehave;

    private GameObject _enemyObj;
    private EnemyBehaviour _enemyBehave;

    void Start()
    {
        _playerObj = GameObject.Find("Player").gameObject;
        _playerBehave = _playerObj.GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        
    }
}
