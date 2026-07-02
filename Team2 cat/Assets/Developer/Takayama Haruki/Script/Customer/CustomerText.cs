using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
using Const;

public class CustomerText : MonoBehaviour
{
    public bool text_next; //テキスト変更用フラグ

    [Header("テキストデータの設定")]
    [SerializeField] TextMeshProUGUI text; //変更するテキスト先
    [SerializeField] TextAsset[] textfile; //読み取るテキストデータ
    [SerializeField] List<string[]> text_data = new List<string[]>(); //テキストファイルのテキストを保存用
    [SerializeField] TextMeshProUGUI book_coment; //図鑑のテキスト

    [Header("ゲームの答え確認用\n設定不可")]
    [SerializeField] private int genre; //答えのジャンル
    [SerializeField] private int poster; //答えの映画

    [Header("文字送りのスピード")]
    public float show_interval; //文字送りのスピード

    private Coroutine execution_text; //文字送り用
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
        if (StartGame.Instance.game_start)
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
                        NextText();
                        poster_switch = true;
                        SaveText();
                    }
                    //答えのポスターが見つかっていなかった場合
                    else if (!poster_switch)
                    {
                        NextText();
                    }

                    //答えのポスターが見つかっていたら
                    if (poster_switch)
                    {
                        //次のヒントにいける場合
                        if (text_next)
                        {
                            text_num++;
                            hint = text_data[text_num][text_count].ToString();

                            text_next = false;

                            SaveText();
                        }
                    }


                }
                else
                    NextText();
            }
            else
            {
                if (poster_switch)
                {
                    hint = "ENDTEXT";
                    return;
                }
                else
                    NextText();
            }
        }
    }

    /// <summary>
    /// 改行用メソッド
    /// </summary>
    void NextText()
    {
        text_num++;
        text_count = 0;
        hint = text_data[text_num][text_count].ToString();
    }


    /// <summary>
    /// お客さんのテキスト変更用メソッド
    /// </summary>
    private void SaveText()
    {
        text.text = hint;
        book_coment.text = hint;

        SoundManager.Instance.PlaySE((int)SoundConst.SE_ID.CUSTOMER_POP, 1.0f);
        StartCoroutine(ShowText()); // コルーチンを開始
    }

    /// <summary>
    /// 文字をゆっくり出す用のコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowText()
    {
        text.ForceMeshUpdate();

        int totalChars = text.textInfo.characterCount;
        text.maxVisibleCharacters = 0;

        for (int i = 0; i <= totalChars; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(show_interval);
        }
    }
}
