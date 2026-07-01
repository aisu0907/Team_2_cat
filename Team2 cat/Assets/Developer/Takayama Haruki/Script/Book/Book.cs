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
    private void Start()
    {
        //図鑑表示リセット
        book_ui.SetActive(false);
    }   

    private void Update()
    {
            CloseBook();

            MouseControll(ref is_hover, not_ui);
    }

    //クリック時の処理
    public override void OnClick()
    {
        if (book_num > 0)
        {
            if (!GameController.Instanse.on_ui && !time.pause)
            {
                GameController.Instanse.on_ui = true;
                time.pause = true;
                book_num--;
                book_ui.SetActive(true); //図鑑を表示
            }
        }
    }

    public void CloseBook()
    {
        //UIが消えた時
        if (!GameController.Instanse.on_ui && time.pause)
        {
            time.pause = false;
            book_ui.SetActive(false); //図鑑を非表示
        }
    }
}