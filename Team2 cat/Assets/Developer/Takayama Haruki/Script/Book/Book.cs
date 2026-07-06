using UnityEngine;
using Const;

public class Book : MouseController
{
    [Header("使用オブジェクトの設定")]
    [SerializeField] GameObject book_ui; //図鑑オブジェクト
    [SerializeField] Timer time; //タイマーオブジェクト
    
    [Header("図鑑の使用回数")]
    public int book_num; //図鑑使用回数

    [Header("サウンド設定")]
    public float open_vlome; //開いた時のSE音量
    public float exit_vlome; //閉じた時のSE音量

    private bool open_book;
    private SoundManager sound; //サウンドインスタンス省略用

    private void Start()
    {
        //図鑑表示リセット
        book_ui.SetActive(false);

        sound = SoundManager.Instance; //省略
    }   

    private void Update()
    {
        //ゲームが始まっていたら
        if (StartGame.Instance.game_start)
        {
            CloseBook(); //図鑑を閉じる

            MouseControll(ref is_hover, not_ui); //マウスコントローラを呼び出す
        }
    }

    //クリック時の処理
    public override void OnClick()
    {
        if (book_num > 0)
        {
            if (!GameController.Instanse.on_ui && !open_book)
            {
                sound.PlaySE((int)SoundConst.SE_ID.BOOK_OPEN, open_vlome); //音を鳴らす
                GameController.Instanse.on_ui = true;
                open_book = true;
                book_num--; //図鑑の使用回数を減らす
                book_ui.SetActive(true); //図鑑を表示
            }
        }
    }

    /// <summary>
    /// 図鑑を閉じる用メソッド
    /// </summary>
    public void CloseBook()
    {
        //UIが消えた時
        if (!GameController.Instanse.on_ui && open_book)
        {
            open_book = false;
            sound.PlaySE((int)SoundConst.SE_ID.EXIT, exit_vlome);
            book_ui.SetActive(false); //図鑑を非表示
        }
    }
}