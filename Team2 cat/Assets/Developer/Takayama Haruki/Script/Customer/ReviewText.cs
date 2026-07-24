using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ReviewText : MonoBehaviour
{
    [Header("テキスト設定")]
    [SerializeField] TextMeshProUGUI review; //テキストを入れるオブジェクト
    [SerializeField] TextAsset review_text_file; //使用するテキストファイル
    [SerializeField] List<string[]> review_text_data = new List<string[]>(); //テキストファイルのテキストを保存用

    private int text_line; //行数管理用
    private int text_word; //文章区切り用
    private string review_text; //1行保存用
    private bool review_switch; //スコア検知用フラグ
    private bool review_end; //テキストの終わり検知用フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //数値リセット
        text_line = 0;
        text_word = 0;

        //フラグリセット
        review_switch = false;
        review_end = false;

        StringReader reader = new StringReader(review_text_file.text); //テキストファイルを取得

        //テキストファイル内のテキストを取得
        while (reader.Peek() != -1)
        {
            //,で区切る
            string line = reader.ReadLine();
            review_text_data.Add(line.Split(','));
        }

        review_text = review_text_data[text_line][text_word].ToString(); //テキストを1行取得

        //テキストの終わりを検出しなかった場合
        while(!review_end)
        {
            if(review_text == ((int)ResultManager.Instance.clear_score).ToString() && !review_switch)
            {
                review_switch = true;

                NextText();
            }
                
            //クリアスコアを見つけたら
            if (review_switch)
            {
                review.text = review_text; //テキスト変更
                
                NextText();

                //テキストの終わりを検出したら
                if (review_text == "ENDTEXT")
                    review_end = true;
            }
            else
                NextText();
        }
    }

    /// <summary>
    /// 次の行数に移動用メソッド
    /// </summary>
    private void NextText()
    {
        text_line++;
        text_word = 0;
        review_text = review_text_data[text_line][text_word].ToString();
    }
}
