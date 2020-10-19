using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ActionRange : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Tilemap _actionRange;
    [SerializeField] TileBase _passibleTile;
    [SerializeField] TileBase _attackbleTile;

    private GameObject _playerObj;
    private PlayerMove _playerMove;

    private Tilemap _ground;
    private Tilemap _ForbiddenGraund;

    void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerMove = _playerObj.GetComponent<PlayerMove>();

        _ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        _ForbiddenGraund = GameObject.Find("ForbiddenGraund").GetComponent<Tilemap>();
    }

    void Update()
    {
        Vector3Int tempPlayerPos = FloorToPosition(_playerObj.transform.position);
        ShowPassibleTile(tempPlayerPos, 2);
    }

    //到達可能のマスの可視化
    void ShowPassibleTile(Vector3Int unitPos, int maxStep)
    {
        CheckPassible(unitPos, maxStep + 1);
    }

    //到達できるかのチェック
    void CheckPassible(Vector3Int pos, int remainStep)
    {
        //if (_actionRange.GetTile(pos) == _ForbiddenGraund)
        //{
            _actionRange.SetTile(pos, _passibleTile);
            --remainStep;
            if (remainStep == 0) return;

            CheckPassible(pos + Vector3Int.up, remainStep);
            CheckPassible(pos + Vector3Int.left, remainStep);
            CheckPassible(pos + Vector3Int.right, remainStep);
            CheckPassible(pos + Vector3Int.down, remainStep);
        //}
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

            //Vector3Int tempPlayerPos = FloorToPosition(_playerObj.transform.position);
            //ShowPassibleTile(tempPlayerPos, 2);

        }
    }

    //小数点以下を切り捨て、整数型に
    private Vector3Int FloorToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.FloorToInt(position.x),
            Mathf.FloorToInt(position.y),
            Mathf.FloorToInt(position.z));

        return afterPosition;
    }
}
