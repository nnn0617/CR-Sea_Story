using UnityEngine;
using System.Collections.Generic;

public class SORO_GameScene : MonoBehaviour
{
    private GameObject _playerObj;
    private PlayerBehaviour _playerBehave;

    private GameObject _enemyObj;
    private EnemyBehaviour _enemyBehave;

    private List<ActorsBehaviour> _actors;
    private ActorsBehaviour _activeActor;

    void Start()
    {
        _actors = new List<ActorsBehaviour>();

        _playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerBehave = _playerObj.GetComponent<PlayerBehaviour>();
        _actors.Add(_playerBehave);

        _enemyObj = GameObject.FindGameObjectWithTag("Enemy").gameObject;
        _enemyBehave = _enemyObj.GetComponent<EnemyBehaviour>();
        _actors.Add(_enemyBehave);
    }

    void Update()
    {
        foreach (var actor in _actors)
        {
            if(actor.GetUnitState == (int)ActorsBehaviour.UnitState.Intercept)
            {
                break;
            }
            actor.UnitUpdate();
        }
        ChangingPhased();
    }

    private void ChangingPhased()
    {

        _activeActor = _playerBehave;
    }
}
