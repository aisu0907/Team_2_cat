using JetBrains.Annotations;
using UnityEngine;

public class MovieSelect : MonoBehaviour
{
    [SerializeField] MovieData[] movie_data; //映画のジャンルデータを入れる
    public SpriteRenderer[] targets; //画像を変更するオブジェクトを入れる
    public int fake; //似た選択肢を入れる回数

    [SerializeField] private int choices_num = 5;
    private Sprite[] fake_answer; //
    private int answer_genre; //答えのジャンル
    private int answer; //答え
    private int rand; //ランダムに取得した数値を保存
    private int[] choices;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choices = new int[choices_num];
        answer_genre = Random.Range(0, movie_data.Length); //答えのジャンルをランダムで取得
        answer = Random.Range(0, movie_data[answer_genre].poster.Length); //答えをランダムで取得
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < fake;)
        {
            rand = Random.Range(0, movie_data[answer_genre].poster.Length); //ランダムで取得した数値を代入

            //取得した数値が答えじゃなかったっ場合
            if (answer != rand )
            {
                //画像が入ってた場合
                if (movie_data[answer_genre].poster[rand] != null)
                {
                    fake_answer[i] = movie_data[answer_genre].poster[rand]; //画像を代入
                    movie_data[answer_genre].poster[rand] = null;
                    i++;
                }
            }
        }
    }
}
