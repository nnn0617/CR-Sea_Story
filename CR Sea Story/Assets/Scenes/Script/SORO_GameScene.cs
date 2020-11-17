using UnityEngine;

public class SORO_GameScene : MonoBehaviour
{
    private GameObject _playerObj;
    private PlayerBehaviour _playerBehave;

    private GameObject _enemyObj;
    private EnemyBehaviour _enemyBehave;

    void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerBehave = _playerObj.GetComponent<PlayerBehaviour>();

        _enemyObj = GameObject.FindGameObjectWithTag("Enemy").gameObject;
        _enemyBehave = _enemyObj.GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        if (_playerBehave.SharedIsMyTurn)
        {
            _playerBehave.UnitUpdate();
        }
        if (_enemyBehave.SharedIsMyTurn)
        {
            _enemyBehave.UnitUpdate();
        }
    }
}
