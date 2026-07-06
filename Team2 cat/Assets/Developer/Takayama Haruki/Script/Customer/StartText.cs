using Const;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StartText : MonoBehaviour
{
    [Header("テキストデータの設定")]
    [SerializeField] TextMeshProUGUI customer_text; //変更するテキスト先
    [SerializeField] TextAsset[] start_text_file; //読み取るテキストデータ
    [SerializeField] List<string[]> start_text_data = new List<string[]>(); //テキストファイルのテキストを保存用

    [Header("文字送りの設定")]
    public float show_interval; //文字送りのスピード
    public float text_cooltime; //次のテキストが出るまでのクールタイム

    [Header("サウンド設定")]
    public float pop_sound_vlome; //テキスト更新時のSE音量

    public bool next_text; //次のテキスト変更用フラグ
    private string start_text; //1行保存用
    private int text_line; //行数管理用
    private int text_word; //文章区切り用
    private bool end_text; //テキストの終了管理用
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //数値リセット
        text_line = 0;
        text_word = 0;

        next_text = true;
        end_text = false;

        StringReader reader = new StringReader(start_text_file[0].text); //テキストファイルを取得

        //テキストファイル内のテキストを取得
        while (reader.Peek() != -1)
        {
            //,で区切る
            string line = reader.ReadLine();
            start_text_data.Add(line.Split(','));
        }

        start_text = start_text_data[text_line][text_word].ToString(); //1行目を取得
    }

    // Update is called once per frame
    void Update()
    {
        //テキストがスタートしていなかったら
        if(StartGame.Instance.text_start)
        {
            //テキストの終わりを検出しなかった場合
            if (start_text != "ENDTEXT")
            {
                //テキストの改行を検出しかった場合
                if (start_text != "NEXT")
                {
                    if (next_text)
                    {
                        SaveText();

                        next_text = false;
                    }
                }
                else
                {
                    NextText();
                }
            }
            else
            {
                if (!end_text) 
                {
                    StartCoroutine(StanbyEffect());
                    end_text = true;
                }
            }
        }
    }

    /// <summary>
    /// 改行用メソッド
    /// </summary>
    private void NextText()
    {
        text_line++;
        text_word = 0;
        start_text = start_text_data[text_line][text_word].ToString();
    }
    
    /// <summary>
    /// お客さんのテキスト変更用メソッド
    /// </summary>
    private void SaveText()
    {
        //テキストオブジェクトにテキストを入れる
        customer_text.text = start_text;

        SoundManager.Instance.PlaySE((int)SoundConst.SE_ID.CUSTOMER_POP, 1.0f);

        StartCoroutine(ShowText()); //コルーチンを開始
    }

    /// <summary>
    /// 文字をゆっくり出す用のコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowText()
    {
        customer_text.ForceMeshUpdate();

        int totalchars = customer_text.textInfo.characterCount;
        customer_text.maxVisibleCharacters = 0;

        for (int i = 0; i <= totalchars; i++)
        {
            customer_text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(show_interval);
        }

        NextText();

        yield return new WaitForSeconds(text_cooltime);
    }

    private IEnumerator StanbyEffect()
    {
        yield return new WaitForSeconds(text_cooltime);

        GameManager.Instance.effect_start = true;
    }
}
