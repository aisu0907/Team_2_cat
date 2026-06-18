using UnityEngine;

public class BookClose : MouseController
{
    [SerializeField] Book book_ui;

    void Update()
    {
        MouseControll(ref hover_click, !not_ui);

        if(!GameController.Instanse.is_click)
        {
            Debug.Log("“®‚¢‚Ä‚é");
            one_click = false;
        }
    }

    public override void OnClick()
    {
        Debug.Log("ƒNƒŠƒbƒN");
        if (!one_click)
        {
            book_ui.CloseBook();
            one_click = true;
        }
    }
}
