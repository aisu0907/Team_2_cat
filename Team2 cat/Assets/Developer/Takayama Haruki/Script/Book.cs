using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject gray;
    public Time time;

    private bool book_on;
    private GameObject gray_save;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        book_on = false;
    }   

    private void Update()
    {
        if(book_on && time.pause)
        {
            book_on = false;
            time.pause = false;
            Destroy(gray_save);
        }
    }

    private void OnMouseDown()
    {
        if(!book_on && !time.pause)
        {
            time.pause = true;
            book_on = true;
            gray_save = Instantiate(gray, new Vector3(0, 0, 0), Quaternion.identity);


        }
    }
}