using UnityEngine;
using UnityEngine.EventSystems;

public class Poster : MonoBehaviour
{
    public GameObject highlight_effect; //ハイライトエフェクトオブジェクト
    public float effect_size_x; //ハイライトエフェクトの横幅
    public float effect_size_y; //ハイライトエフェクトの縦幅
    public float poster_size_up_x;//大きくするサイズ横幅
    public float poster_size_up_y;//大きくするサイズ縦幅

    private SpriteRenderer poster; //ポスター画像
    private GameObject highlight_effect_save; //ハイライトエフェクト一時保存用
    private bool highlight;     //ハイライト切り替え用
    private Vector3 effect_size;//ハイライトエフェクト保存用
    private Vector3 poster_size_save;//ポスターの大きさ保存用

    void Start()
    {
        //リセット
        highlight = true;

        effect_size = new Vector3(effect_size_x, effect_size_y, 0); //座標を設定
        poster_size_save = gameObject.transform.localScale;
        poster = gameObject.GetComponent<SpriteRenderer>(); //画像をセット
    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
        }
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
        if (highlight == true)
        {
            //ハイライトオブジェクトを生成
            //if (highlight_effect_save == null)
            //    highlight_effect_save = Instantiate(highlight_effect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity); //オブジェクトを生成
            //highlight_effect_save.transform.localScale = effect_size + gameObject.transform.localScale; //大きさを設定 

            //ポスターの大きさを変更
            gameObject.transform.localScale = (poster_size_save + new Vector3(poster_size_up_x, poster_size_up_y, 0.0f));
        }

        highlight = false;
    }

    //オブジェクトの上からカーソルがなくなったとき
    private void OnMouseExit()
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
