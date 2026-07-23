using System.Collections;
using TMPro;
using UnityEngine;
using Const;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] MovieData[] movie_data;//映画のデータ
    [SerializeField] RectTransform[] target_posters; //移動させるポスターの位置
    [SerializeField] FadeManager fade_obj; //フェードするオブジェクト

    [Header("ポスター演出系")]
    public Vector2[] poster_target_pos; //ポスターが移動する場所
    public float move_speed; //ポスターが動く速度

    [Header("その他")]
    public TMP_Text moveText;//画面に残り手数を出すUI
    public int moves = 2;//手数
   
    public bool move_scene;  //シーン移動管理用フラグ
    public bool effect_start;//エフェクトの開始管理用フラグ
    public bool effect_end;  //エフェクトの終了管理用フラグ

    public bool is_game_over { get; private set; }//二重にシーン移動しないようにする
    public static float result_time { get; private set; }

    private int ans_genre; //答えのジャンル
    private int ans_poster;//答えのポスター
    private bool is_game_clear;//ゲームクリア判定
    private SoundManager sound; //サウンドインスタンス省略用

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
        effect_start = false;
        effect_end = false;
        move_scene = true;

        //数値リセット
        result_time = 0;

        ans_genre = MovieSelect.Instance.Answergenre(); //答えのジャンルを取得
        ans_poster = MovieSelect.Instance.Answer(); //答えを取得

        sound = SoundManager.Instance;

        UpdateUI();//ゲーム開始時に一度UIを表示
    }

    private void Update()
    {
        if(effect_start && !effect_end)
        {
            StartCoroutine(StartMovePoster());
            effect_start = false;
        }
    }


    /// <summary>
    /// 手数の減少処理用メソッド
    /// </summary>
    public void UseMove()
    {
        if (moves > 0)
        {
            moves--; //手数を減らす
        }

        if (moves <= 0 && !is_game_clear)//0以下になったら
        {

            moves = 0;//マイナスにならないように
            UpdateUI();

            // 手数切れによるゲームオーバー呼び出し
            if (!is_game_over)
            {
                GameOver();
            }
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
        if (!is_game_over)
        {
            Debug.Log("ゲームオーバーが呼ばれました！");//デバック用

            is_game_over = true;//ゲームオーバー状態にする

            ScoreManager.result_score = 1.0f;

            fade_obj.StartFadeOut(move_scene, SceneName.GAMEOVER);

        }
    }

    /// <summary>
    /// ゲームクリア判定用メソッド
    /// </summary>
    /// <param name="ans">判定する画像</param>
    public bool GameClear(Sprite ans)
    {
        //ゲームがクリアされてなかったら
        if (!is_game_clear)
        {
            result_time = Timer.Instance.game_time;

            //画像が正解だったら
            if (ans == movie_data[ans_genre].poster[ans_poster])
            {
                is_game_clear = true;

                Debug.Log("ゲームクリアが呼ばれました！");//デバック用

                UseMove();
                fade_obj.StartFadeOut(move_scene, SceneName.RESULT);

                return true;
            }
            else
            {
                sound.PlaySE((int)SoundConst.SE_ID.MISS, 1.0f); //
                ScoreManager.Instance.ScoreDown();
                UseMove();
            }
        }

        return false;
    }


    /// <summary>
    /// ポスター全体の移動を管理する用メソッド
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMovePoster()
    {
        for(int i= 0; i < target_posters.Length; i++)
        {
            //ポスターの移動をする
            yield return StartCoroutine(MovePoster(i));

            //指定された時間待つ
            yield return new WaitForSeconds(0.1f);
        }

        effect_end = true;//エフェクトを終了
        StartGame.Instance.game_start = true;

        yield return new WaitForSeconds(1.0f);
        StartGame.Instance.ObjectActive();

    }

    /// <summary>
    /// ポスターを移動させる用メソッド
    /// </summary>
    /// <param name="obj_id">移動さえるオブジェクトの配列番号</param>
    /// <returns></returns>
    IEnumerator MovePoster(int obj_id)
    {
        // 目的地との距離がほぼ0になるまでループ
        while (Vector2.Distance(target_posters[obj_id].anchoredPosition, poster_target_pos[obj_id]) > 0.01f)
        {
            // 前回の移動処理と同じ（Time.deltaTimeを掛ける）
            target_posters[obj_id].anchoredPosition = Vector3.MoveTowards(target_posters[obj_id].anchoredPosition, poster_target_pos[obj_id], move_speed * Time.deltaTime);

            // 1フレーム待ってから、次のフレームでwhile文の先頭に戻る
            yield return null;
        }
    }
}  
