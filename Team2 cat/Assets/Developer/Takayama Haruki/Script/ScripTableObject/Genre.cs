using UnityEngine;

public enum Genre
{
    Horror,
    Comedy,
    LoveComedy,
    HumanDrama,
}

[CreateAssetMenu(fileName = "MovieData", menuName = "Scriptable Objects/MovieData")]
public class MovieData : ScriptableObject
{
    public Genre genre;
    public Sprite[] poster;
}
