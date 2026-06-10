using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public bool on_close; //画面を閉じる用フラグ

    private  GameControllerAction controller;//input syestem

    public static GameController Instanse; //シングルトン
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instanse = this;

        on_close = false;
        controller = new GameControllerAction();//input syestem設定
    }

    private void OnEnable()
    {
        controller.Enable();
        controller.Game.Close.performed += OnPause; 
    }

    private void OnDisable()
    {
        controller.Disable();
        controller.Game.Close.performed -= OnPause;
    }

    /// <summary>
    /// on_close管理用メソッド
    /// </summary>
    /// <param name="context"></param>
    private void OnPause(InputAction.CallbackContext context)
    {
        on_close = true;
    }
}