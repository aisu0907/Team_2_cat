using UnityEngine;
using Const;

public class Book : MouseController
{
    [Header("使用オブジェクトの設定")]
    [SerializeField] GameObject book_ui; //図鑑オブジェクト
    [SerializeField] Time time; //タイマーオブジェクト
    [Space(CodeAsset.SPACE)]
    [Header("図鑑の使用回数")]
    public int book_num; //図鑑使用回数

    private bool book_on; //図鑑表示管理用フラグ

    private void Start()
    {
        book_on = false;
        book_ui.SetActive(false);
    }   

    private void Update()
    {
        if (GameController.Instanse.on_close)
            if (book_on && time.pause)
            {
                book_on = false;
                time.pause = false;
                book_ui.SetActive(false);
                GameController.Instanse.on_close = false;
            }

        MouseControll(ref is_hover);

    }

    //クリック時の処理
    public override void OnClick()
    {
        if (book_num > 0)
        {
            if (!book_on && !time.pause)
            {
                time.pause = true;
                book_on = true;
                book_num--;
                book_ui.SetActive(true);
            }
        }
    }
}