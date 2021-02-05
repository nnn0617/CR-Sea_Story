using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Tilemap _attackRange;
    [SerializeField] TileBase _attackbleTile;

    private GameObject[] _playerObjects;
    private PlayerBehaviour _playerBehave;
    private List<PlayerBehaviour> _players;

    private GameObject[] _enemyObjects;
    private EnemyBehaviour _enemyBehave;
    private List<EnemyBehaviour> _enemys;

    void Start()
    {
        _playerObjects = GameObject.FindGameObjectsWithTag("Player");
        _players = new List<PlayerBehaviour>();
        foreach(var player in _playerObjects)
        {
            _players.Add(player.GetComponent<PlayerBehaviour>());
        }

        _enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        _enemys = new List<EnemyBehaviour>();
        foreach (var enemy in _enemyObjects)
        {
            _enemys.Add(enemy.GetComponent<EnemyBehaviour>());
        }
    }

    void Update()
    {
        foreach(var player in _players)
        {
            foreach (var enemy in _enemys)
            {
                Vector3Int tempPlayerPos = FloorToPosition(player.transform.position);
                Vector3Int tempEnemyPos = FloorToPosition(enemy.transform.position);
                tempEnemyPos.z = tempPlayerPos.z;
                ShowAtackbleTile(tempPlayerPos, tempEnemyPos, player.attackRange);
            }
        }
    }

    //攻撃可能のマスの可視化
    void ShowAtackbleTile(Vector3Int unitPos,Vector3Int enemyPos, int maxStep)
    {
        CheckAtackble(unitPos, enemyPos, maxStep + 1);
    }

    //攻撃できるかのチェック
    void CheckAtackble(Vector3Int pos, Vector3Int epos, int remainStep)
    {
        foreach (var player in _players)
        {
            if (player.GetAttackFlag)
            {
                if (epos == pos)//攻撃範囲に敵がいるかどうか
                {
                    _attackRange.SetTile(pos, _attackbleTile);//攻撃可能範囲表示
                }
                --remainStep;
                if (remainStep == 0) return;
            }
            else
            {
                _attackRange.SetTile(pos, null);//攻撃可能範囲非表示
                --remainStep;
                if (remainStep == 0) return;
            }

            //再帰してremainStep分チェック&表示or非表示
            CheckAtackble(pos + Vector3Int.up, epos, remainStep);
            CheckAtackble(pos + Vector3Int.left, epos, remainStep);
            CheckAtackble(pos + Vector3Int.right, epos, remainStep);
            CheckAtackble(pos + Vector3Int.down, epos, remainStep);
        }
    }

    //float型の小数点以下を切り捨て、int(整数)型に
    private Vector3Int FloorToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.FloorToInt(position.x),
            Mathf.FloorToInt(position.y),
            Mathf.FloorToInt(position.z));

        return afterPosition;
    }
}
