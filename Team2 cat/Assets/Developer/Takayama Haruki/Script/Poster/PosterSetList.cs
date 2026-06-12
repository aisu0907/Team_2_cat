using UnityEngine;
using Const;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PosterSetList : MonoBehaviour
{
    [Header("使用オブジェクト")]
    [SerializeField] MovieData[] poster_data; //ScripTableObject
    [SerializeField] GameObject null_poster; //空のポスター
    [SerializeField] Transform content; //ポスターを置く場所

    [Header("ポスターセット設定")]
    public Vector3 start_pos; //初期位置
    public Vector2 plus_space; //ポスターの配置間隔
    public int set_num; //一度に置く数

    private GameObject[] null_poster_save; //ポスター保存用
    private Vector3 poster_pos; //ポスターを配置する場所
    private int max_set_num; //ポスターを置く数
    private int count; 
    private bool set_start; //ポスターセット開始管理用
    private bool set_end; 
    private int object_num;
    private void OnEnable()
    {
        if(!set_end)
           set_start = true;

        if (set_start)
        {
            //リセット
            count = 0;
            object_num = 0;

            poster_pos = start_pos; //ポスターの配置座標を作成

            //設置するポスターの数を決める
            for (int i = 0; i < poster_data.Length; i++)
            {
                max_set_num += poster_data[i].poster.Length;
                Debug.Log(max_set_num);
            }

            //配列の数を指定
            null_poster_save = new GameObject[max_set_num];

            Debug.Log("画像を配置します");
            //ポスターを配置
            for (int genre = 0; genre < poster_data.Length; genre++)
                for (int poster = 0; poster < poster_data[genre].poster.Length; poster++)
                {

                    //指定された数ポスターを配置したら
                    if (count > set_num)
                    {
                        //位置をリセット
                        poster_pos.y += plus_space.y;
                        poster_pos.x = start_pos.x;
                        count = 0;
                    }

                    null_poster_save[object_num] = Instantiate(null_poster, content); //ポスターを生成
                    null_poster_save[object_num].GetComponent<RectTransform>().anchoredPosition = poster_pos; //ポスターの位置を指定
                    null_poster_save[object_num].GetComponent<Image>().sprite = poster_data[genre].poster[poster]; //ポスターの画像を設定
                    var movie_data = null_poster_save[object_num].GetComponent<BookPosterSelect>(); //BookPosterSelectを取得
                    movie_data.genre_id =  genre; //ジャンルを取得
                    movie_data.poster_id = poster;//ポスターを取得

                    poster_pos.x += plus_space.x;  //x位置を変更

                    count++;

                    if (object_num > max_set_num)
                        object_num++;

                    Debug.Log("配置に成功しました");
                }

            set_end = true;
            set_start = false;
        }
    }
}
