using UnityEngine;
using System.Collections.Generic;
using ActorsState;

public class SORO_GameScene : MonoBehaviour
{
    private GameObject[] _playerObjects;
    private PlayerBehaviour _playerBehave;
    private GameObject[] _enemyObjects;
    private EnemyBehaviour _enemyBehave;

    private List<ActorsBehaviour> _actors;
    private ActorsBehaviour _activeActor;

    void Start()
    {
        _actors = new List<ActorsBehaviour>();

        _playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in _playerObjects)
        {
            _actors.Add(player.GetComponent<PlayerBehaviour>());
        }

        _enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in _enemyObjects)
        {
            _actors.Add(enemy.GetComponent<EnemyBehaviour>());
        }
    }

    void Update()
    {
        foreach (var actor in _actors)
        {
            actor.UnitUpdate();
        }
        //ChangingPhased();
    }

    private void ChangingPhased()
    {
        _activeActor = _playerBehave;
    }
}
