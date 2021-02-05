using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MoveRange : MonoBehaviour
{
    [SerializeField] Tilemap _moveRange;
    [SerializeField] TileBase _passibleTile;
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
        foreach (var player in _playerObjects)
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
        foreach (var player in _players)
        {
            foreach (var enemy in _enemys)
            {
                Vector3Int tempPlayerPos = FloorToPosition(player.transform.position);
                Vector3Int tempEnemyPos = FloorToPosition(enemy.transform.position);
                tempEnemyPos.z = tempPlayerPos.z;
                ShowAttackbleTile(tempPlayerPos, player.moveRange + player.attackRange);
                ShowPassibleTile(tempPlayerPos, player.moveRange);
            }
        }
    }

    //到達可能のマスの可視化
    void ShowPassibleTile(Vector3Int unitPos, int maxStep)
    {
        CheckPassible(unitPos, maxStep + 1);
    }

    //攻撃可能なマスの可視化
    void ShowAttackbleTile(Vector3Int unitPos, int maxStep)
    {
        CheckAttackble(unitPos, maxStep + 1);
    }

    //到達できるかのチェック
    void CheckPassible(Vector3Int pos, int remainStep)
    {
        foreach (var player in _players)
        {
            if (player.GetSelectionFlag)
            {
                _moveRange.SetTile(pos, _passibleTile);//移動可能範囲表示
                --remainStep;
                if (remainStep == 0) return;
            }
            else
            {
                _moveRange.SetTile(pos, null);//移動可能範囲削除
                --remainStep;
                if (remainStep == 0) return;
            }

            //再帰してremainStep分チェック&表示or非表示
            CheckPassible(pos + Vector3Int.up, remainStep);
            CheckPassible(pos + Vector3Int.left, remainStep);
            CheckPassible(pos + Vector3Int.right, remainStep);
            CheckPassible(pos + Vector3Int.down, remainStep);
        }
    }

    void CheckAttackble(Vector3Int pos, int remainStep)
    {
        foreach (var player in _players)
        {
            if (player.GetSelectionFlag)
            {
                _moveRange.SetTile(pos, _attackbleTile);//攻撃可能範囲表示
                --remainStep;
                if (remainStep == 0) return;
            }
            else
            {
                _moveRange.SetTile(pos, null);//攻撃可能範囲非表示
                --remainStep;
                if (remainStep == 0) return;
            }

            //再帰してremainStep分チェック&表示or非表示
            CheckAttackble(pos + Vector3Int.up, remainStep);
            CheckAttackble(pos + Vector3Int.left, remainStep);
            CheckAttackble(pos + Vector3Int.right, remainStep);
            CheckAttackble(pos + Vector3Int.down, remainStep);
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
