using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CustomerText : MonoBehaviour
{
    public bool text_nexst;

    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] TextAsset TextFile;
    private int text_num;
    private int count;

    private string genre;
    private int poster;
    List<string[]> text_data = new List<string[]>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poster = MovieSelect.Instance.Answer();
        StringReader reader = new StringReader(TextFile.text);
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
    }

    // Update is called once per frame
    void Update()
    {
        string hint = text_data[text_num][count].ToString();

        bool genre_switch = false;
        bool poster_switch = false;
        if (hint != "ENDTEXT")
        {
            if (hint != genre && !genre_switch)
                genre_switch = true;
            else
                text_num++;
            if (hint != poster.ToString() && genre_switch && !poster_switch)
                poster_switch = true;
            else
                text_num++;


            if (genre_switch)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    count++;
                }
            }
        }
        else
        {

        }
    }
}
