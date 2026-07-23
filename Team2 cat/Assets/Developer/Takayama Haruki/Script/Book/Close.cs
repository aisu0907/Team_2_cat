using UnityEngine;

public class BookClose : MouseController
{
    [SerializeField] Book book_ui; //図鑑オブジェクト 

    private void Awake()
    {  
        not_ui = true;
    }

    void Update()
    {
        MouseControll(ref is_hover, not_ui); //マウス操作を呼ぶ
    }

    //クリック時処理
    public override void OnClick()
    {
        GameController.Instanse.on_ui = false;
        book_ui.CloseBook(); //図鑑を閉じる
    }
}
