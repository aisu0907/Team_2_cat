using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Poster : MouseController
{
    [Header("使用オブジェクト")]
    [SerializeField] GameObject highlight_effect; //ハイライトエフェクトオブジェクト

    [Header("スタートエフェクト設定")]
    public float poster_pos_x;
    public float poster_pos_y;
    public float poster_speed;

    [Header("エフェクト設定")]
    public float effect_size_x; //ハイライトエフェクトの横幅
    public float effect_size_y; //ハイライトエフェクトの縦幅
    public float poster_up_size_rate; //大きくするエフェクトの拡大率

    private Image poster; //ポスター画像

    private bool effect_end;
    private GameObject highlight_effect_save; //ハイライトエフェクト一時保存用
    private bool highlight;     //ハイライト切り替え用
    private Vector3 effect_size;//ハイライトエフェクト保存用
    private Vector3 poster_up_size;  //拡大後サイズ保存用
    private Vector3 poster_size_save;//ポスターの大きさ保存用

    private void Start() 
    {
        //リセット
        highlight = true;
        effect_end = false;
        //not_ui = true;

        poster = gameObject.GetComponent<Image>(); //画像をセット
        poster_size_save = gameObject.transform.localScale; //ポスターの元のサイズを保存
        poster_up_size = gameObject.transform.localScale * poster_up_size_rate;
        effect_size = new Vector3(effect_size_x, effect_size_y, 0); //座標を設定
    }

    private void Update()
    {
        if(effect_end)
        {
        MouseControll(ref is_hover, not_ui);

        if (!GameController.Instanse.is_click)
            one_click = false;
        }
    }

    //クリック時の処理
    public override void OnClick()
    {
        if (!one_click)
        {
            //クリア判定
            GameManager.Instance.Gameclear(poster.sprite);
            one_click = true;
        }
    }

    //カーソルが上にある時の処理
    public override void OnEnter()
    {
        if (highlight == true)
        {
            //ハイライトオブジェクトを生成
            if (highlight_effect_save == null)
                highlight_effect_save = Instantiate(highlight_effect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity); //オブジェクトを生成
            highlight_effect_save.transform.localScale = effect_size + gameObject.transform.localScale; //大きさを設定 

            //ポスターの大きさを変更
            gameObject.transform.localScale = poster_up_size;
                //(poster_size_save + new Vector3(poster_size_up_x, poster_size_up_y, 0.0f));
        }

        highlight = false;
    }

    //カーソルがいなくなったときの処理
    public override void OnExit()
    {
        if (highlight == false)
        {
            //ハイライトオブジェクトを削除
            Destroy(highlight_effect_save);

            //ポスターの大きさをリセット
            gameObject.transform.localScale = poster_size_save;
        }

        highlight = true;

    }
}
