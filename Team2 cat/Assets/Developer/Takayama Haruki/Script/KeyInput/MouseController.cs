using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MouseController : MonoBehaviour
{
    /// <summary>
    /// マウスのホバー状態を検知する用メソッド
    /// </summary>
    /// <param name="cam">そのシーンのカメラ</param>
    /// <param name="is_hovered">マウスのホバー状態</param>
    public void MouseControll(Camera cam, ref bool is_hovered)
    {
        //UIに触れていた場合
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return; 
        }

        Vector2 mouse_pos = Mouse.current.position.ReadValue(); //マウスの位置を取得
        Vector2 world_pos = cam.ScreenToWorldPoint(mouse_pos); //マウスの座標をワールド座標に変換

        RaycastHit2D hit = Physics2D.Raycast(world_pos, Vector2.zero); //マウスが当たったものを取得

        //当たっているものが自分自身か確認
        bool now_hovered = (hit.collider != null && hit.collider.gameObject == gameObject); 

        // カーソルが当たった時
        if (now_hovered && !is_hovered )
        {
            OnEnter();
        }

        // カーソルが当たっている状態でクリックした時（未完成）
        if(now_hovered && !is_hovered )
        {

        }
        // カーソルが外れた時
        if (!now_hovered && is_hovered)
        {
            OnExit();
        }

        is_hovered = now_hovered;
    }

    /// <summary>
    /// カーソルが当たっている状態でクリックした時の処理用メソッド
    /// </summary>
    public virtual void OnClick(){}

    /// <summary>
    /// カーソルが当たった時の処理用メソッド
    /// </summary>
    public virtual void OnEnter(){}

    /// <summary>
    /// カーソルが外れた時の処理用メソッド
    /// </summary>
    public virtual void OnExit(){}
}
