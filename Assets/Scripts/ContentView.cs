using UnityEngine;

public class ContentView : MonoBehaviour
{
    public void HideAll()
    {
        gameObject.SetActive(false);
    }

    public void ShowAll()
    {
        gameObject.SetActive(true);
    }
}
