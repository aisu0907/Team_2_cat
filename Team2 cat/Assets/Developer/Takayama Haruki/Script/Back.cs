using UnityEngine;

public class Back : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("ƒNƒŠƒbƒN‚³‚ê‚½: " + hit.collider.name);

                // ‚±‚±‚Åˆ—
            }
        }
    }


}
