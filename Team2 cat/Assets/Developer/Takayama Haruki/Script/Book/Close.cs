using UnityEngine;

public class BookClose : MouseController
{
    [SerializeField] Book book_ui;

    void Update()
    {
        MouseControll(ref is_hover, not_ui);

        if(!GameController.Instanse.is_click)
        {
            one_click = false;
        }
    }

    public override void OnClick()
    {
        if (!one_click)
        {
            Debug.Log("ƒNƒŠƒbƒN");
            GameController.Instanse.on_ui = false;
            book_ui.CloseBook();
            one_click = true;
        }
    }
}
