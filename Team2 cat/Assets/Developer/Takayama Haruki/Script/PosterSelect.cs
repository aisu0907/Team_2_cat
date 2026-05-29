using UnityEngine;

public class Poster : MonoBehaviour
{
    public GameObject highlight_effect; //ハイライトエフェクトオブジェクト
    public float effect_size_x; //ハイライトエフェクトの横幅
    public float effect_size_y; //ハイライトエフェクトの縦幅

    private SpriteRenderer poster; //ポスター画像
    private bool highlight; //ハイライト切り替え用
    private GameObject higlight_effect_save; //ハイライトエフェクト一時保存用
    private Vector3 effect_size; //ハイライトエフェクト保存用

    void Start()
    {
        //リセット
        highlight = true;

        effect_size = new Vector3(effect_size_x, effect_size_y, 0); //座標を設定
        poster = gameObject.GetComponent<SpriteRenderer>(); //画像をセット
    }

    //オブジェクトがクリックされたとき
    private void OnMouseDown()
    {
        //クリア判定
        GameManager.Instance.Gameclear(poster.sprite);
    }

    //オブジェクトの上にカーソルがあるとき
    private void OnMouseEnter()
    {
        if(highlight == true)
        {
            //ハイライトオブジェクトを生成
            higlight_effect_save = Instantiate(highlight_effect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity); //オブジェクトを生成
            higlight_effect_save.transform.localScale = effect_size + gameObject.transform.localScale; //大きさを設定
            Instantiate(higlight_effect_save); //オブジェクトを出現
        }

        highlight = false;
    }

    //オブジェクトの上からカーソルがなくなったとき
    private void OnMouseExit()
    {
        if (highlight == false)
        {
            //ハイライトオブジェクトを削除
            Destroy(higlight_effect_save);
        }

        highlight = true;
    }
}
