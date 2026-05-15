using UnityEngine;
using UnityEngine.SceneManagement;//シーンを切り替え機能を使うのに必要

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;//どこからでも呼べるようにする

    private bool isGameOver = false;//二重にシーン移動しないようにする

    void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        Debug.Log("ゲームオーバーが呼ばれました！");//デバック用
        if (isGameOver)//ゲームオーバー処理中ならこれ以降は無視
            return;

        isGameOver = true;//ゲームオーバー状態にする

        SceneManager.LoadScene("two Scene");
    }
}
