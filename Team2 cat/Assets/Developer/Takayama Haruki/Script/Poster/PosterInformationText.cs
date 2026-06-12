using TMPro;
using UnityEngine;

public class PosterInformationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] movie_information_data;
    private void SetMovieDataText(string[][] movie_data)
    {
        for(int i = 0; i < movie_information_data.Length; i++)
        {
           // movie_information_data = movie_data[i];

        }
    }
}
