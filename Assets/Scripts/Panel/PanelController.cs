using System.Collections;
using TMPro;
using UnityEngine;

// Class for updating panel with actual info decoded from QR
[RequireComponent(typeof(RaycastManager))]
public class PanelController : MonoBehaviour
{
    private const float ScanningSessionsDelay = 0.5f;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private QRScanner _QRScanner;
    [SerializeField] private Transform _container;
    [SerializeField] private Panel _panelPrefab;

    private RaycastManager _raycastManager;
    private Panel _panel;

    private void Start()
    {
        _raycastManager = GetComponent<RaycastManager>();
        StartCoroutine(Scan());
    }

    // Try decode camera view
    private IEnumerator Scan()
    {
        while (true)
        {
            string text = _QRScanner.DecodeScreenshot();

            _text.text = text;             // TODO: remove
            UpdatePanel(text);

            yield return new WaitForSeconds(ScanningSessionsDelay);
        }
    }

    private void UpdatePanel(string text)
    {
        if (_panel == null)
        {
            Vector3 position = _raycastManager.TryGetRaycastPosition();

            if (position != default)
            {
                _panel = Instantiate(_panelPrefab, position, Quaternion.identity, _container);
            }
        }

        // Set data to panel
    }
}
