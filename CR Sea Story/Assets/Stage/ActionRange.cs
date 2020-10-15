using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ActionRange : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Tilemap _actionRange;
    [SerializeField] TileBase _passibleTile;
    [SerializeField] TileBase _attackbleTile;

    private PlayerMove _playerMove;

    void Start()
    {
        _playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    //到達可能のマスの可視化
    void ShowPassibleTile(Vector3Int unitPos, int maxStep)
    {
        CheckPassible(unitPos, maxStep + 1);
    }

    //到達できるかのチェック
    void CheckPassible(Vector3Int pos, int remainStep)
    {
        //if ()
        //{

        //}
        _actionRange.SetTile(pos, _passibleTile);
        --remainStep;
        if (remainStep == 0) return;

        CheckPassible(pos + Vector3Int.up, remainStep);
        CheckPassible(pos + Vector3Int.left, remainStep);
        CheckPassible(pos + Vector3Int.right, remainStep);
        CheckPassible(pos + Vector3Int.down, remainStep);
    }

    //クリック時の処理
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_playerMove.GetSelectionFlag)
        {
            TileBase clickedTile = _actionRange.GetTile(_playerMove.GetMouseClickPos);
            if (clickedTile == _passibleTile)
            {

            }
            if (clickedTile == _attackbleTile)
            {

            }
            //else if (クリックした地点にまだ行動できるユニットがいる)
            //{
            //その地点にいるユニットを選択状態にする処理

            //ShowPassibleTile(ユニットの地点、ユニットの移動可能な距離);
            //}
        }
    }
}
