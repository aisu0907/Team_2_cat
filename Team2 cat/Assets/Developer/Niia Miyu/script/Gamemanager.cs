using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] MovieData[] movie_data;//映画のデータ
    public static GameManager Instance;//どこからでもゲームマネージャーを使える
    public TMP_Text moveText;//画面に残り手数を出すUI
    public Time timer;//時間を測っているスクリプト
    public int moves = 2;//手数

    private int ans_genre; //答えのジャンル
    private int ans_poster;//答え
    private bool isgameclear;
    private bool isgameover;//二重にシーン移動しないようにする
    //public GameOverManager gameOverManager;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        isgameclear = false;
        isgameover = false;
        ans_genre = MovieSelect.Instance.Answergenre();
        ans_poster = MovieSelect.Instance.Answer();
        UpdateUI();//ゲーム開始時に一度UIを表示
    }

    private void Update()
    {
        if(!isgameover)
        {
            // 時間のスクリプトが存在、残り時間が0以下
            if (timer != null && timer.game_time <= 0)
            {
                isgameover = true;

                GameOver();
            }
        }
    }


    /// <summary>
    /// 手数の減少処理用メソッド
    /// </summary>
    public void UseMove()
    {
        if(moves > 0)
            moves--; //手数を減らす


        if (moves <= 0 && !isgameclear)//0以下になったら
        {

            moves = 0;//マイナスにならないように
            UpdateUI();

            // 手数切れによるゲームオーバー呼び出し
            if (!isgameover)
                GameOver();
        }
        else
        {
            UpdateUI();//まだ残っていれば数字だけ更新
        }
    }


    /// <summary>
    /// 手数の表示更新用メソッド
    /// </summary>
    void UpdateUI()
    {
        if (moveText != null)
        {
            moveText.text = "残り手数 : " + moves;
        }
    }

    /// <summary>
    /// ゲームオーバー後の処理用メソッド
    /// </summary>
    public void GameOver()
    {
        Debug.Log("ゲームオーバーが呼ばれました！");//デバック用

        isgameover = true;//ゲームオーバー状態にする

        SceneManager.LoadScene("two Scene");//シーン移動
    }

    /// <summary>
    /// ゲームクリア判定用メソッド
    /// </summary>
    /// <param name="ans">判定する画像</param>
    public void Gameclear(Sprite ans)
    { 
        //ゲームがクリアされていないかつ画像が正解だったら
        if(ans == movie_data[ans_genre].poster[ans_poster] && !isgameclear)
        {
            isgameclear = true;
            Debug.Log("ゲームクリアが呼ばれました！");//デバック用
            UseMove();
        }
        else
            UseMove();
    }
}  
