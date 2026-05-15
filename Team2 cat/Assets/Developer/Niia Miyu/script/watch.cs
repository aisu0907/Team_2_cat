using UnityEngine;

public class Watch : MonoBehaviour
{
    public Time timer;//時間を測っているスクリプト

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver)//すでにゲームオーバーなら何もしない
            return;

        // 時間のスクリプトが存在、残り時間が0以下
        if (timer !=null&&timer.game_time <= 0)
        {
            isGameOver = true;

            GameOverManager.Instance.GameOver();
        }
    }
}
