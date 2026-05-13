using UnityEngine;

using TMPro;

public class MovesUI : MonoBehaviour
{
    public TextMeshProUGUI movesText;

    public void UpdateUI(int moves)
    {
        movesText.text = "éËêî: " + moves;//ï\é¶
    }
}
