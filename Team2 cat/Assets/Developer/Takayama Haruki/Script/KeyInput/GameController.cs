using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public bool on_close;//画面を閉じる用フラグ
    public bool is_click { get; private set; } //クリックを管理する用フラグ

    private GameControllerAction controller;//input syestem

    public static GameController Instanse; //シングルトン
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instanse = this;

        //フラグリセット
        on_close = false;
        is_click = false;

        controller = new GameControllerAction();//input syestem設定
    }

    private void OnEnable()
    {
        controller.Enable();
        //ESCキー判定
        controller.Game.Close.started += OnPauseStart;
        controller.Game.Close.canceled += OnPauseEnd;

        controller.Game.Select.started += OnMouseClickStart;
        controller.Game.Select.canceled += OnMouseClickEnd;

    }

    private void OnDisable()
    {
        controller.Disable();
        //ESCキー判定
        controller.Game.Close.started -= OnPauseStart;
        controller.Game.Close.canceled -= OnPauseEnd;

        controller.Game.Select.started -= OnMouseClickStart;
        controller.Game.Select.canceled -= OnMouseClickEnd;
    }

    // on_close管理用メソッド
    private void OnPauseStart(InputAction.CallbackContext context) => on_close = true;
    private void OnPauseEnd(InputAction.CallbackContext context) => on_close = false;

    //is_click管理用メソッド
    private void OnMouseClickStart(InputAction.CallbackContext context) => is_click = true;
    private void OnMouseClickEnd(InputAction.CallbackContext context) => is_click = false;

}