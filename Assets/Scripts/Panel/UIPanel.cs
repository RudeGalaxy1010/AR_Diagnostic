using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetText(string text)
    {
        _button.GetComponentInChildren<TMP_Text>().text = text;
    }

    private void OnButtonClick()
    {
        _button.GetComponentInChildren<TMP_Text>().text = "Clicked";
    }
}
