using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MouseController : MonoBehaviour
{
    public bool not_ui = false; //UIの上にいるかどうか
    public bool is_hover = false;//マウスのホバー状態
    public bool hover_click = false;//クリックを長押ししているどうか
    public bool one_click = false;

    /// <summary>
    /// マウスの状態を検知する用メソッド
    /// </summary>
    /// <param name="is_hovered">前フレームのマウスのホバー状態</param>
    /// <param name="ui">使用オブジェクトがUIを無視するか</param>
    public void MouseControll(ref bool is_hovered, bool ui)
    {
        Camera cam = Camera.main;

        //UI判定がいる場合
        if(!ui)
        {
            //UIに触れていた場合
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("UIの上にいます");
                OnExit();
                return;
            }
        }

        Vector2 mouse_pos = Mouse.current.position.ReadValue(); //マウスの位置を取得
        Vector2 world_pos = cam.ScreenToWorldPoint(mouse_pos); //マウスの座標をワールド座標に変換

        RaycastHit2D hit = Physics2D.Raycast(world_pos, Vector2.zero); //マウスが当たったものを取得

        //当たっているものが自分自身か確認
        bool now_hovered = (hit.collider != null && hit.collider.gameObject == gameObject); //現フレームのマウスのホバー状態

        //カーソルが当たった時
        if (now_hovered && !is_hovered )
        {
            OnEnter();
        }

        //マウスがクリックされていない時
        if (!GameController.Instanse.is_click)
        {
            //カーソルが当たっている状態でクリックした時
            if (now_hovered && !hover_click)
            {
                OnClick();
            }

            hover_click = true;
        }
        else
            hover_click = false;

        //カーソルが外れた時
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
