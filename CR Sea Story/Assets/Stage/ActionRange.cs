using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ActionRange : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Tilemap _actionRange;
    [SerializeField] TileBase _passibleTile;
    [SerializeField] TileBase _attackbleTile;

    //到達可能のマスの可視化
    void ShowPassibleTile(Vector3Int pos, int maxStep)
    {
        
    }

    //到達できるかのチェック
    void CheckPossible(Vector3Int pos, int remainStep)
    {
        
    }

    //クリック時の処理
    public void OnPointerClick(PointerEventData pointerEventData)
    {

    }
}
