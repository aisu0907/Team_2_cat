using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject gray; //エフェクトオブジェクト
    public Time time; //タイマーオブジェクト
    public int book_num; //図鑑使用回数

    private bool book_on; //図鑑表示管理用フラグ
    private GameObject gray_save; //オブジェクト一時保存用

    private void Start()
    {
        book_on = false;
    }   

    private void Update()
    {
        if (GameController.Instanse.on_close)
            if (book_on && time.pause)
            {
                book_on = false;
                time.pause = false;
                GameController.Instanse.on_close = false;
                Destroy(gray_save);
            }
    }

    private void OnMouseDown()
    {
        if(book_num > 0){ 
            if (!book_on && !time.pause)
            {
                time.pause = true;
                book_on = true;
                gray_save = Instantiate(gray, new Vector3(0, 0, 0), Quaternion.identity);
                book_num--;

            }
        }
    }
}