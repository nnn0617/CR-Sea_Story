using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Tilemap _attackRange;
    [SerializeField] TileBase _attackbleTile;

    private GameObject _playerObj;
    private PlayerMove _playerMove;

    void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerMove = _playerObj.GetComponent<PlayerMove>();
    }

    void Update()
    {
        Vector3Int tempPlayerPos = FloorToPosition(_playerObj.transform.position);
        ShowAtackbleTile(tempPlayerPos, _playerMove.attackRange);
    }

    //到達可能のマスの可視化
    void ShowAtackbleTile(Vector3Int unitPos, int maxStep)
    {
        CheckAtackble(unitPos, maxStep + 1);
    }

    //攻撃できるかのチェック
    void CheckAtackble(Vector3Int pos, int remainStep)
    {
        if (_playerMove.GetAttackFlag)
        {
            _attackRange.SetTile(pos, _attackbleTile);//攻撃可能範囲表示
            --remainStep;
            if (remainStep == 0) return;
        }
        else
        {
            _attackRange.SetTile(pos, null);//攻撃可能範囲削除
            --remainStep;
            if (remainStep == 0) return;
        }

        //再帰してremainStep分チェック&表示or非表示
        CheckAtackble(pos + Vector3Int.up, remainStep);
        CheckAtackble(pos + Vector3Int.left, remainStep);
        CheckAtackble(pos + Vector3Int.right, remainStep);
        CheckAtackble(pos + Vector3Int.down, remainStep);
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
