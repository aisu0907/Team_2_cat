using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Const;

public class BookPosterSelect : MouseController
{
    [Header("テキスト設定")]
    [SerializeField] TextAsset[] movie_text_file; //使用するテキストファイル
    [SerializeField] List<string[]> movie_text_data = new List<string[]>(); //テキストファイルのテキストを保存用
    
    [Header("使用オブジェクト")]
    [SerializeField] GameObject highlight_effect; //ハイライトエフェクトオブジェクト

    [Header("ポスターID\n設定不可")]
    public int genre_id = 0;  //ポスターのジャンル
    public int poster_id = 0; //ポスターの使用ポスター

    [Header("ポスターエフェクト設定")]
    public float effect_size_x; //ハイライトエフェクトの横幅
    public float effect_size_y; //ハイライトエフェクトの縦幅

    private string movie_text; //1行保存用
    private int text_line; //行数管理用
    private int text_word; //文章区切り用
    private bool set_informatinon; //情報セット管理用フラグ
    private bool set_genre; //ジャンルセット管理用フラグ
    private bool set_title; //タイトルセット管理用フラグ
    private bool set_summary; //映画概要セット管理用フラグ

    [SerializeField] private string[] movie_information;
    private GameObject highlight_effect_save; //ハイライトエフェクト一時保存用
    private bool highlight;     //ハイライト切り替え用
    private Vector3 effect_size;//ハイライトエフェクト保存用

    void Start()
    {
        //フラグリセット
        not_ui = true;
        set_informatinon = false;
        set_genre = false;
        set_title = false;
        set_summary = false;
        hover_click = true;

        //数値リセット
        text_line = 0;
        text_word = 0;
        //文字列リセット
        movie_information = new string[3];


        StringReader reader = new StringReader(movie_text_file[genre_id].text); //テキストファイルを取得

        //テキストファイル内のテキストを取得
        while (reader.Peek() != -1)
        {
            //,で区切る
            string line = reader.ReadLine();
            movie_text_data.Add(line.Split(','));
        }

        movie_text = movie_text_data[text_line][text_word].ToString(); //テキストを1行取得

        while (!set_genre || !set_title || !set_summary)
        {
            //文字列がジャンルの場合
            if (movie_text == "GENRE" && !set_genre)
            {
                text_word++; //1行進める
                movie_information[PosterConst.GENRE] = ("ジャンル : " + movie_text_data[text_line][text_word].ToString()); //ジャンルを取得
                set_genre = true;
                NextText();
            }

            //数字が一致してかつポスターが見つかっていなかったら
            if (movie_text == poster_id.ToString() && !set_informatinon)
                set_informatinon = true;
            //ポスターが見つかっていなかった場合
            else if (!set_informatinon)
                NextText();

            //ポスターが見つかっていた場合
            if (set_informatinon)
            {
                //入ってる文字がTITLEかつタイトルを見つけていない場合
                if (movie_text == "TITLE" && !set_title)
                {
                    text_word++; //1行進める
                    movie_information[PosterConst.TITLE] = ("タイトル : " + movie_text_data[text_line][text_word]); //タイトルを取得
                    set_title = true;
                    NextText();
                }

                //入ってる文字がSUMMARYかつ概要を見つけていない場合
                if (movie_text == "SUMMARY" && !set_summary)
                {
                    NextText();

                    //入ってる文字がENDTEXTになるまでループ
                    while(movie_text != "ENDTEXT")
                    {
                        movie_information[PosterConst.SUMMARY] += movie_text; //テキストを1行追加
                        movie_information[PosterConst.SUMMARY] += "\n"; //改行を追加
                        NextText();
                        Debug.Log(movie_information[PosterConst.SUMMARY]);
                    }

                    //入ってる文字がENDTEXTなら
                    if (movie_text == "ENDTEXT")
                    {
                        set_title = true;
                        break;
                    }
                }

                NextText();
            }
        }
    }

    void Update()
    {
        MouseControll(ref is_hover, not_ui);

        //クリックされていない時
        if(!GameController.Instanse.is_click)
        {
            one_click = false;
        }
    }

    public override void OnClick()
    {
        if (!one_click)
        {
            SoundManager.Instance.PlaySE((int)SoundConst.SE_ID.SELECT, 1.0f);
            Debug.Log("クリック確認");
            PosterInformationText.Instanse.SetMovieDataText(movie_information);
            one_click = true;
        }
    }

    /// <summary>
    /// 次の行数に移動用メソッド
    /// </summary>
    private void NextText()
    {
        text_line++;
        text_word = 0;
        movie_text = movie_text_data[text_line][text_word].ToString();
    }

    ////カーソルが上にある時の処理
    //public override void OnEnter()
    //{
    //    if (highlight == true)
    //    {
    //        //ハイライトオブジェクトを生成
    //        if (highlight_effect_save == null)
    //            highlight_effect_save = Instantiate(highlight_effect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity); //オブジェクトを生成
    //        highlight_effect_save.transform.localScale = effect_size + gameObject.; //大きさを設定 
    //    }

    //    highlight = false;
    //}

    ////カーソルがいなくなったときの処理
    //public override void OnExit()
    //{
    //    if (highlight == false)
    //    {
    //        //ハイライトオブジェクトを削除
    //        Destroy(highlight_effect_save);

    //    }

    //    highlight = true;

    //}
}
