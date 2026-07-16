using UnityEngine;
using UnityEngine.UI;
using Const;
using System.Collections;

public class Poster : MouseController
{
    [Header("使用オブジェクト")]
    [SerializeField] GameObject highlight_effect; //ハイライトエフェクトオブジェクト

    [Header("エフェクト設定")]
    public float poster_up_size_rate; //大きくするエフェクトの拡大率
    public Color poster_click_color;  //ポスタークリック時の色
    public float poster_blinking_time;//ポスターの色が変わる時間
    public int blinking_count; //点滅回数

    [Header("サウンド設定")]
    public float poster_cursoe_vlome; //カーソルを合わせた時のSE音量

    private Image poster; //ポスター画像
    private SoundManager sound; //サウンドインスタンス省略用
    private Vector3 poster_up_size;  //拡大後サイズ保存用
    private Vector3 poster_size_save;//ポスターの大きさ保存用
    private bool effect_end; //エフェクトの状態管理用
    private void Start() 
    {
        //フラグリセット
        effect_end = true;

        //初期設定
        poster = gameObject.GetComponent<Image>(); //画像をセット
        poster_size_save = gameObject.transform.localScale; //ポスターの元のサイズを保存
        poster_up_size = gameObject.transform.localScale * poster_up_size_rate;　//大きくするサイズを設定

        sound = SoundManager.Instance; //省略
    }

    private void Update()
    {
        //演出が終了していたら
        if(StartGame.Instance.game_start)
        {
            MouseControll(ref is_hover, not_ui); //マウス操作を呼ぶ
        }
    }

    //クリック時の処理
    public override void OnClick()
    {
        //エフェクトが終了していたら
        if (effect_end)
        {
            effect_end = false;

            StartCoroutine(PosterClickEffect()); //エフェクトスタート

            GameManager.Instance.GameClear(poster.sprite); //クリアチェック
        }
    }

    //カーソルが上にある時の処理
    public override void OnEnter()
    {
        sound.PlaySE((int)SoundConst.SE_ID.POSTER_CURSOR, poster_cursoe_vlome); //音を鳴らす
                    
        //ポスターの大きさを変更
        gameObject.transform.localScale = poster_up_size;
    }

    //カーソルがいなくなったときの処理
    public override void OnExit()
    {
        //ポスターの大きさをリセット
        gameObject.transform.localScale = poster_size_save;
    }

    /// <summary>
    /// ポスタークリック時の演出を処理するようコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator PosterClickEffect()
    {
        Color start_color = poster.color;

        for (int i = 0; i < blinking_count; i++)
        {
            poster.color = poster_click_color; //ポスターの色を変更

            yield return new WaitForSeconds(poster_blinking_time); //指定時間待つ

            poster.color = start_color; //最初の色に戻す

            yield return new WaitForSeconds(poster_blinking_time); //指定時間待つ
        }

        effect_end = true; 

    }
}
