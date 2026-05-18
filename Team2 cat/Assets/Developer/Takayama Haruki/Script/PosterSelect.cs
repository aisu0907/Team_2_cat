using UnityEngine;

public class PosterSelect : MonoBehaviour
{
    private SpriteRenderer poster;

    void Start()
    {
        poster = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        //ƒNƒŠƒA”»’è
        GameManager.Instance.Gameclear(poster.sprite);
    }
}
