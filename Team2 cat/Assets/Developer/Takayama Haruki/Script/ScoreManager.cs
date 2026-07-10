using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("スコア設定")]
    [Tooltip("減らすスコア")]
    public float minus_score; //減らすスコア

    [Tooltip("減らすスコア")]
    public float game_score;

    public static float result_score; //ゲームスコア

    public static ScoreManager Instance; //シングルトン

    private void Awake()
    {
        Instance = this; //シングルトン
    }

    /// <summary>
    /// スコア減少用メソッド
    /// </summary>
    public void ScoreDown()
    {
        game_score -= minus_score;
        result_score = game_score;
    }
}
