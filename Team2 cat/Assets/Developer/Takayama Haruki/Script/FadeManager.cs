using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UIを操作するために必要

public class FadeManager : MonoBehaviour
{
    public float fade_duration; //フェードにかかる時間

    private Image fade_img; //変更するオブジェクトのImage

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //コンポーネントを設定
        fade_img = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// フェードイン用メソッド
    /// </summary>
    /// <param name="move_scene">シーン移動管理用</param>
    /// <param name="scene_name">移動するシーン</param>
    public void StartFadeIn(bool move_scene = false, string scene_name = "noname_scene")
    {
        //コルーチンをスタート
        StartCoroutine(FadeCoroutine(1.0f, 0.0f, move_scene, scene_name));
    }

    /// <summary>
    /// フェードアウト用メソッド
    /// </summary>
    /// <param name="move_scene">シーン移動管理用</param>
    /// <param name="scene_name">移動するシーン</param>
    public void StartFadeOut(bool move_scene = false, string scene_name = "noname_scene")
    {
        //コルーチンをスタート
        StartCoroutine(FadeCoroutine(0.0f, 1.0f, move_scene, scene_name));
    }


    /// <summary>
    /// フェード処理用コルーチン
    /// </summary>
    /// <param name="start_alpha">開始時のオブジェクトのアルファ値</param>
    /// <param name="end_alpha">終了時のオブジェクトのアルファ値</param>
    /// <param name="scene_load">シーン移動管理用</param>
    /// <param name="scene_name">移動するシーンの名前</param>
    /// <returns></returns>
    private IEnumerator FadeCoroutine(float start_alpha, float end_alpha, bool scene_load, string scene_name)
    {
        float elapsed_time = 0f;

        Color color = fade_img.color; //オブジェクトのカラーを取得

        while (elapsed_time < fade_duration)
        {
            elapsed_time += Time.deltaTime;

            // Mathf.Lerpを使って、時間の経過に合わせてアルファ値を補間する
            float currentAlpha = Mathf.Lerp(start_alpha, end_alpha, elapsed_time / fade_duration);
            
            color.a = currentAlpha;
            fade_img.color = color;

            yield return null; // 1フレーム待つ
        }

        //目標の色に完全に合わせる
        color.a = end_alpha;
        fade_img.color = color;

        //シーン移動する場合
        if (scene_load)
        {
            yield return new WaitForSeconds(0.2f); //0.5秒待つ
            SceneManager.LoadScene(scene_name); //シーン移動
        }
    }
}
