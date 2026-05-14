using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CustomerText : MonoBehaviour
{
    public bool text_next;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextAsset textfile;
    private int text_num;
    private int count;

    [SerializeField] private string genre;
    [SerializeField] private int poster;
    [SerializeField] List<string[]> text_data = new List<string[]>();
    [SerializeField] private string hint;
    [SerializeField] private bool genre_switch = false;
    [SerializeField] private bool poster_switch = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poster = MovieSelect.Instance.Answer(); //“š‚¦‚ðŽæ“¾
        text_num = 0;
        count = 0;
        text_next = false;
        StringReader reader = new StringReader(textfile.text);
        int answer_genre = MovieSelect.Instance.Answergenre();
        
        
        switch (answer_genre)
        {
            case 0:
                genre = "COMEDY";
                break;
            case 1:
                genre = "HOLLOL";
                break;
            case 2:
                genre = "LOVEROMANCE";
                break;
            case 3:
                genre = "";
                break;

        }
            
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            text_data.Add(line.Split(','));
        }

        hint = text_data[text_num][count].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (hint != "ENDTEXT")
        {
            if (hint == genre && !genre_switch)
                genre_switch = true;

            if (hint == poster.ToString() && genre_switch && !poster_switch)
                poster_switch = true;

            if (!genre_switch || !poster_switch)
            {
                text_num++;
                hint = text_data[text_num][count].ToString();
                Debug.Log(hint);
            }

            if (poster_switch)
            {
                if (hint != "NEXT")
                {
                    if(text_next)
                    {
                        count++;
                        hint = text_data[text_num][count].ToString();

                        text_next = false;
                    }

                    text.text = hint;
                }
            }

        }
        else
        {

        }
    }
}
