using JetBrains.Annotations;
using UnityEngine;

public class MovieSelect : MonoBehaviour
{
    [SerializeField] MovieData[] movie_data; //映画のジャンルデータを入れる
    public SpriteRenderer[] targets; //画像を変更するオブジェクトを入れる
    public int fake; //似た選択肢を入れる回数

    private int answer_genre; //答えのジャンル
    private int answer; //答え
    private int[] choices = new int[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        answer_genre = Random.Range(0, movie_data.Length); //答えのジャンルをランダムで取得
        answer = Random.Range(0, movie_data[answer_genre].poster.Length); //答えをランダムで取得
    }

    // Update is called once per frame
    void Update()
    {
        foreach(int choices in choices)
        {
           
        }
    }
}
