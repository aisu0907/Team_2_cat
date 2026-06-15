using TMPro;
using UnityEngine;

public class PosterInformationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] movie_information_data; //テキストを入れるオブジェクト
    public static PosterInformationText Instanse;
    private void Awake()
    {
        Instanse = this;
    }

    public void SetMovieDataText(string[] movie_data)
    {
        for (int i = 0; i < movie_information_data.Length; i++)
        {
            Debug.Log(movie_data[i]);
            movie_information_data[i].text = movie_data[i];
        }
    }
}
