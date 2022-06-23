using System.Collections;
using TMPro;
using UnityEngine;

public class QRTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private QRScanner _QRScanner;
    [SerializeField] private PanelCreator _panelCreator;

    private UIPanel _panel;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        while (true)
        {
            string text = _QRScanner.DecodeScreenshot();
            _text.text = text;

            if (string.IsNullOrEmpty(text) == false)
            {
                UpdatePanel(text);
            }
            else
            {
                Destroy(_panel.gameObject);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdatePanel(string text)
    {
        if (_panel == null)
        {
            _panel = _panelCreator.TryCreatePanel();
        }

        _panel.SetText(text);
    }
}
