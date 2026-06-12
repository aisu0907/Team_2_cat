using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookPosterSelect : MouseController
{
    [Header("テキスト設定")]
    [SerializeField] TextAsset[] movie_text;
    [SerializeField] List<string[]> text_data = new List<string[]>(); //テキストファイルのテキストを保存用

    [Header("使用オブジェクト")]
    [SerializeField] GameObject highlight_effect; //ハイライトエフェクトオブジェクト

    [Header("ポスターID\n設定不可")]
    public int genre_id = 0;
    public int poster_id = 0;

    [Header("ポスターエフェクト設定")]
    public float effect_size_x; //ハイライトエフェクトの横幅
    public float effect_size_y; //ハイライトエフェクトの縦幅

    private GameObject highlight_effect_save; //ハイライトエフェクト一時保存用
    private bool highlight;     //ハイライト切り替え用
    private Vector3 effect_size;//ハイライトエフェクト保存用

    void Start()
    {
        not_ui = true;
        
        
    }

    void Update()
    {
        MouseControll(ref is_hover, not_ui);
    }

    public override void OnClick()
    {
        Debug.Log("クリック確認");
          
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
