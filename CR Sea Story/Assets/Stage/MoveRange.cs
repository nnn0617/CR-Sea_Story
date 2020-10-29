using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveRange : MonoBehaviour
{
    [SerializeField] Tilemap _moveRange;
    [SerializeField] TileBase _passibleTile;
    [SerializeField] TileBase _attackbleTile;

    private GameObject _playerObj;
    private PlayerBehaviour _playerBehave;

    void Start()
    {
        _playerObj = GameObject.Find("Player").gameObject;
        _playerBehave = _playerObj.GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        Vector3Int tempPlayerPos = FloorToPosition(_playerObj.transform.position);
        ShowAttackbleTile(tempPlayerPos, _playerBehave.moveRange + _playerBehave.attackRange);
        ShowPassibleTile(tempPlayerPos, _playerBehave.moveRange);
    }

    //到達可能のマスの可視化
    void ShowPassibleTile(Vector3Int unitPos, int maxStep)
    {
        CheckPassible(unitPos, maxStep + 1);
    }

    void ShowAttackbleTile(Vector3Int unitPos, int maxStep)
    {
        CheckAttackble(unitPos, maxStep + 1);
    }

    //到達できるかのチェック
    void CheckPassible(Vector3Int pos, int remainStep)
    {
        if (_playerBehave.GetSelectionFlag)
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

    void CheckAttackble(Vector3Int pos, int remainStep)
    {
        if (_playerBehave.GetSelectionFlag)
        {
            _moveRange.SetTile(pos, _attackbleTile);//攻撃可能範囲表示
            --remainStep;
            if (remainStep == 0) return;
        }
        else
        {
            _moveRange.SetTile(pos, null);//攻撃可能範囲削除
            --remainStep;
            if (remainStep == 0) return;
        }

        CheckAttackble(pos + Vector3Int.up, remainStep);
        CheckAttackble(pos + Vector3Int.left, remainStep);
        CheckAttackble(pos + Vector3Int.right, remainStep);
        CheckAttackble(pos + Vector3Int.down, remainStep);
    }

    private Vector3Int FloorToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.FloorToInt(position.x),
            Mathf.FloorToInt(position.y),
            Mathf.FloorToInt(position.z));

        return afterPosition;
    }
}
