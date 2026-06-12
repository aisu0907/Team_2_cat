using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CustomerText : MonoBehaviour
{
    public bool text_next; //テキスト変更用フラグ

    [Header("テキストデータの設定")]
    [SerializeField] TextMeshProUGUI text; //変更するテキスト先
    [SerializeField] TextAsset[] textfile; //読み取るテキストデータ
    [SerializeField] List<string[]> text_data = new List<string[]>(); //テキストファイルのテキストを保存用

    [Header("ゲームの答え確認用")]
    [SerializeField] private int genre; //答えのジャンル
    [SerializeField] private int poster; //答えの映画

    private int text_num; //テキスト行数
    private int text_count; //テキストの区切り
    private string hint; //テキストを1行保存用
    private bool poster_switch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_num = 0; //行数をリセット
        text_count = 0;
        text_next = false; //フラグをリセット
        poster_switch = false; //フラグをリセット
        poster = MovieSelect.Instance.Answer(); //答えを取得
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
        //テキストの終わりを検出しなかった場合
        if (hint != "ENDTEXT")
        {
            //テキストの改行を検出しかった場合
            if (hint != "NEXT")
            {
                //答えのポスターかつ答えのポスターが見つかっていなかった場合
                if (hint == poster.ToString() && !poster_switch)
                {
                    Savetext();
                    poster_switch = true;
                    Debug.Log("成功");
                }

                //答えのポスターが見つかっていなかった場合
                if (!poster_switch)
                {
                    Savetext();
                    Debug.Log(hint);
                }

                //答えのポスターが見つかっていたら
                if (poster_switch)
                {
                    //次のヒントにいける場合
                    if (text_next)
                    {
                        text_count++;
                        hint = text_data[text_num][text_count].ToString();

                        text_next = false;
                    }

                    //入ってる文字がNEXTじゃない場合
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
                hint = "ENDTEXT";
                return;
            }
            else
                Savetext();
        }
    }

    /// <summary>
    /// 改行用メソッド
    /// </summary>
    void Savetext()
    {
        text_num++;
        text_count = 0;
        hint = text_data[text_num][text_count].ToString();
    }
}
