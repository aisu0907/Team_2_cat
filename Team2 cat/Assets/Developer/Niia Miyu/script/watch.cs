using UnityEngine;

public class TimeWatcher : MonoBehaviour
{
    public Time timer;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver)
            return;

        // ŠÔØ‚êŠÄ‹
        if (timer.game_time <= 0)
        {
            isGameOver = true;

            GameOverManager.Instance.GameOver();
        }
    }
}
