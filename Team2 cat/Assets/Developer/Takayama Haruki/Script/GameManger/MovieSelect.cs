using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MovieSelect : MonoBehaviour
{
    [SerializeField] MovieData[] movie_data; //映画のジャンルデータを入れる
    [SerializeField] SpriteRenderer[] targets; //画像を変更するオブジェクトを入れる
    public int fake; //似た選択肢を入れる回数
    public int choices_num;
    
    [SerializeField] private int answer_genre; //答えのジャンル
    [SerializeField] private int answer; //答え
    [SerializeField] private Sprite[] choices; //選択肢
     private Sprite[] choices_save; //選択肢を一時的に保存用
    
    private HashSet<string> used = new HashSet<string>(); //同じ画像を使わないよう管理用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        choices = new Sprite [choices_num]; //配列の数を指定
        choices_save = new Sprite[choices_num]; //配列の数を指定
        answer_genre = Random.Range(0, movie_data.Length); //答えのジャンルをランダムで取得
        answer = Random.Range(0, movie_data[answer_genre].poster.Length); //答えをランダムで取得
        choices_save[0] = movie_data[answer_genre].poster[answer]; //答えの画像を取得
    }

    // Update is called once per frame
    void Start()
    {
        //フェイクが設定されていた場合
        if(fake > 0)
            //fakeの数分偽物の答えを用意する
            for (int i = 1; i < fake + 1;)
            {
                int rand_poster = Random.Range(0, movie_data[answer_genre].poster.Length); //ランダムでポスターを取得

                //取得した数値が答えじゃなかったっ場合
                if (answer != rand_poster)
                {
                    //画像が入ってた場合
                    if (!Isused(answer_genre, rand_poster))
                    {
                        Setimage(i, answer_genre, rand_poster); //画像をセット
                        i++;
                    }
                }
            }

        //答え以外の選択肢を埋める
        for(int i = fake + 1; i < choices_num;)
        {
            int rand_genre = Random.Range(0, movie_data.Length); //ランダムでジャンルを取得

            //取得したジャンルが答えのジャンルじゃなかった場合
            if(answer_genre != rand_genre)
            {
                int rand_poster = Random.Range(0, movie_data[rand_genre].poster.Length); //ランダムでポスターを取得
                
                //画像が入ってた場合
                if (!Isused(rand_genre, rand_poster))
                {
                    Setimage(i, rand_genre, rand_poster); //画像をセット
                    i++;
                }
            }
        }

        //答えをシャッフルして入れる
        for (int i = 0; i < choices.Length;)
        {
            int rand = Random.Range(0, choices_save.Length); //ランダムで配列の場所を取得

            //choicesに画像が入っていないかつchoices_saveに画像が入っている場合
            if (choices[i] == null && choices_save[rand] != null)
            {
                choices[i] = choices_save[rand]; //画像をセット
                choices_save[rand] = null; //元のデータを消す
                i++;
            }
        }

        //ターゲットに画像を貼り付ける
        for(int i = 0; i < choices.Length; i++)
        {
            targets[i].sprite = choices[i];
        }
    }

    /// <summary>
    /// 選択肢を埋めるための画像を取得する用メソッド
    /// </summary>
    /// <param name="num">配列の数</param>
    /// <param name="genre">ジャンル</param>
    /// <param name="poster">取得する画像</param>
    void Setimage(int num,int genre,int poster)
    {
        Debug.Log("画像をセット");
        choices_save[num] = movie_data[genre].poster[poster]; //画像をセット
        Markused(genre, poster);
    }

    /// <summary>
    /// 画像が使用されているかを確認用メソッド
    /// </summary>
    /// <param name="genre">ジャンル</param>
    /// <param name="poster">確認する画像</param>
    /// <returns></returns>
    bool Isused(int genre, int poster)
    {
        return used.Contains($"{genre}_{poster}");
    }

    /// <summary>
    /// 画像を使用済に追加する用メソッド
    /// </summary>
    /// <param name="genre">ジャンル</param>
    /// <param name="poster">追加する画像</param>
    void Markused(int genre, int poster)
    {
        used.Add($"{genre}_{poster}");
    }
}
