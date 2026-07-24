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

    [Header("サウンド設定")]
    public float pop_sound_vlome; //テキスト更新時のSE音量

    [Header("文字送りのスピード")]
    public float show_interval; //文字送りのスピード

    private int text_line; //テキスト行数
    private int text_word; //テキストの区切り
    private string hint;   //テキストを1行保存用
    private bool poster_switch; //ポスター検知用フラグ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //数値リセット
        text_line = 0;
        text_word = 0;

        //フラグをリセット
        text_next = false; 
        poster_switch = false;

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

        hint = text_data[text_line][text_word].ToString(); //テキストを1行取得
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームがスタートしていたら
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
                            NextText();

                            text_next = false;

                            if (hint != "ENDTEXT")
                                SaveText();
                        }
                    }
                }
                else
                    NextText();
            }
            else
            {
                //ポスターを見つけていた場合
                if (poster_switch)
                {
                    //hint = "ENDTEXT";
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
        text_line++;
        text_word = 0;
        hint = text_data[text_line][text_word].ToString();
    }


    /// <summary>
    /// お客さんのテキスト変更用メソッド
    /// </summary>
    private void SaveText()
    {
        text.text = hint;
        book_coment.text = hint;

        SoundManager.Instance.PlaySE((int)SoundConst.SE_ID.CUSTOMER_POP, pop_sound_vlome);
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
