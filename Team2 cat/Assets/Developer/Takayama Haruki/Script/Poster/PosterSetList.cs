using UnityEngine;
using Const;

public class PosterSetList : MonoBehaviour
{
    [SerializeField] MovieData[] poster_data;
    public GameObject null_poster; //空のポスター
    public Transform content; //ポスターを置く場所
    public Vector2 start_pos; //初期位置
    public int set_num; //一度に置く数

    private GameObject[] null_poster_save; //ポスター保存用
    private Vector3 poster_pos;
    private int max_set_num; //ポスターを置く数
    void Start()
    {
        poster_pos = new Vector3(start_pos.x, start_pos.y, 0);

        //ポスターを置く数を指定
        for(int i = 0; poster_data.Length > i; i++)
        {
            max_set_num += poster_data[i].poster.Length;
        }

        //配列の数を指定
        null_poster_save = new GameObject[max_set_num];

        //ポスターを配置
        for(int i = 0; i < max_set_num; i++){
            for (int j = 0; j < poster_data.Length ; j++)
            {
                for (int k = 0; k < set_num ; k++)
                {
                    null_poster_save[i] = Instantiate(null_poster);
                    null_poster_save[i].transform.position = poster_pos;
                    var sprite = null_poster_save[i].GetComponent<Sprite>();

                }
            }
        }
    }
}
