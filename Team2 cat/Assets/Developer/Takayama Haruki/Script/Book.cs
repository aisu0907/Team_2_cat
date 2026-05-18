using UnityEngine;
using UnityEngine.Audio;

public class Book : MonoBehaviour
{
    public GameManager gray;
    public Time time;
    private bool book_on;

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
        }
    }

    private void OnMouseDown()
    {
        if(!book_on && !time.pause)
        {
            time.pause = true;
            book_on = true;
        }
    }
}
