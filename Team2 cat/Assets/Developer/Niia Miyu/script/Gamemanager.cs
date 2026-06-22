using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Const;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] MovieData[] movie_data;//映画のデータ
    public TMP_Text moveText;//画面に残り手数を出すUI
    public Time timer;//時間を測っているスクリプト
    public int moves = 2;//手数
    public static int score;

    private int ans_genre; //答えのジャンル
    private int ans_poster;//答えのポスター
    private bool is_game_clear;
    private bool is_game_over;//二重にシーン移動しないようにする

    public static GameManager Instance;//どこからでもゲームマネージャーを使える
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //フラグをリセット
        is_game_clear = false;
        is_game_over = false;

        ans_genre = MovieSelect.Instance.Answergenre(); //答えのジャンルを取得
        ans_poster = MovieSelect.Instance.Answer(); //答えを取得
        UpdateUI();//ゲーム開始時に一度UIを表示
    }

    private void Update()
    {
        if(!is_game_over)
        {
            // 時間のスクリプトが存在、残り時間が0以下
            if (timer != null && timer.game_time <= 0)
            {
                is_game_over = true;

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


        if (moves <= 0 && !is_game_clear)//0以下になったら
        {

            moves = 0;//マイナスにならないように
            UpdateUI();

            // 手数切れによるゲームオーバー呼び出し
            if (!is_game_over)
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

        is_game_over = true;//ゲームオーバー状態にする

        SceneManager.LoadScene(SceneName.GAMEOVER);//シーン移動
    }

    /// <summary>
    /// ゲームクリア判定用メソッド
    /// </summary>
    /// <param name="ans">判定する画像</param>
    public void Gameclear(Sprite ans)
    {
        //ゲームがクリアされてなかったら
        if (!is_game_clear)
        {
            //画像が正解だったら
            if (ans == movie_data[ans_genre].poster[ans_poster])
            {
                is_game_clear = true;
                Debug.Log("ゲームクリアが呼ばれました！");//デバック用
                UseMove();
                SceneManager.LoadScene(SceneName.RESULT);
            }
            else
                UseMove();
        }
    }
}  
