using UnityEngine;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour
{
    public GameObject book_ui; //エフェクトオブジェクト
    public Time time; //タイマーオブジェクト
    public int book_num; //図鑑使用回数

    private bool book_on; //図鑑表示管理用フラグ
    private GameObject book_ui_save; //オブジェクト一時保存用

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
                GameController.Instanse.on_close = false;
                //Destroy(book_ui_save);
                book_ui.SetActive(false);
            }
    }

    private void OnMouseDown()
    {
        if (book_num > 0){ 
            if (!book_on && !time.pause)
            {
                time.pause = true;
                book_on = true;
                book_num--;
                book_ui.SetActive(true);
                //book_ui_save = Instantiate(book_ui);
                //book_ui_save.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                //book_ui_save.GetComponent<Canvas>().worldCamera = Camera.main;

            }
        }
    }
}