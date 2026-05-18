using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CustomerText : MonoBehaviour
{
    public bool text_next; //テキスト変更用フラグ

    [SerializeField] TextMeshProUGUI text; //変更するテキスト先
    [SerializeField] TextAsset[] textfile; //読み取るテキストデータ
    private int text_num; //テキスト行数
    private int text_count; //テキストの区切り

    [SerializeField] private int genre; //答えのジャンル
    [SerializeField] private int poster; //答えの映画s
    [SerializeField] List<string[]> text_data = new List<string[]>(); //テキストファイルのテキストを保存用
    [SerializeField] private string hint; //テキストを1行保存用
    [SerializeField] private bool poster_switch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poster = MovieSelect.Instance.Answer(); //答えを取得
        text_num = 0; //行数をリセット
        text_next = false; //フラグをリセット
        poster_switch = false; //フラグをリセット
        genre = MovieSelect.Instance.Answergenre(); //答えのジャンルを取得
        StringReader reader = new StringReader(textfile[genre].text); //テキストファイルを取得

        //テキストファイル内のテキストを取得
        while (reader.Peek() != -1)
        {
            //,で区切る
            string line = reader.ReadLine();
            text_data.Add(line.Split(','));
        }

        hint = text_data[text_num][text_count].ToString(); //テキストを1行取得
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの終わりを検出するまで
        if (hint != "ENDTEXT")
        {
            if (hint != "NEXT")
            {
                if (hint == poster.ToString() && !poster_switch)
                {
                    Savetext();
                    poster_switch = true;
                    Debug.Log("成功");
                }

                if (!poster_switch)
                {
                    Savetext();
                    Debug.Log(hint);
                }

                if (poster_switch)
                {

                    if (text_next)
                    {
                        text_count++;
                        hint = text_data[text_num][text_count].ToString();

                        text_next = false;
                    }

                    if(hint != "NEXT")
                    text.text = hint;
                }
            }
            else
                Savetext();
        }
        else
        {
            if (poster_switch)
            {
                hint = "";
                return;
            }
            else
                Savetext();
        }
    }

    void Savetext()
    {
        text_num++;
        hint = text_data[text_num][0].ToString();
    }
}
