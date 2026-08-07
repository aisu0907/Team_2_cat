using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PosterInformationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] movie_information_data; //テキストを入れるオブジェクト
    [SerializeField] ScrollbarController scrollbar; //リセットするスクロールバー

    public static PosterInformationText Instanse; //シングルトン
    private void Awake()
    {
        Instanse = this; //シングルトン
    }

    /// <summary>
    /// テキストオブジェクトに入れる用メソッド
    /// </summary>
    /// <param name="movie_data"></param>
    public void SetMovieDataText(string[] movie_data)
    {
        scrollbar.BarPosReset();

        //テキストオブジェクトの数分ループ
        for (int i = 0; i < movie_information_data.Length; i++)
        {
            Debug.Log(movie_data[i]);
            movie_information_data[i].text = movie_data[i];
        }
    }
}
