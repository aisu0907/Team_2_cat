using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public bool on_ui;
    public bool is_click { get; private set; } //クリックを管理する用フラグ

    private GameControllerAction controller;//input syestem

    public static GameController Instanse; //シングルトン
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instanse = this;

        //フラグリセット
        on_ui = false;
        is_click = false;

        controller = new GameControllerAction();//input syestem設定
    }

    private void OnEnable()
    {
        controller.Enable();

        //ESCキー判定
        controller.Game.Close.started += OnPauseStart;

        controller.Game.Select.started += OnMouseClickStart;
        controller.Game.Select.canceled += OnMouseClickEnd;

    }

    private void OnDisable()
    {
        //ESCキー判定
        controller.Game.Close.started -= OnPauseStart;

        controller.Game.Select.started -= OnMouseClickStart;
        controller.Game.Select.canceled -= OnMouseClickEnd;

        controller.Disable();
    }

    // on_close管理用メソッド
    private void OnPauseStart(InputAction.CallbackContext context)
    {
        if(on_ui)
            on_ui = false;
    }

    //is_click管理用メソッド
    private void OnMouseClickStart(InputAction.CallbackContext context) => is_click = true;
    private void OnMouseClickEnd(InputAction.CallbackContext context) => is_click = false;

}