using UnityEngine;

// Class for updating panel with actual info decoded from QR
[RequireComponent(typeof(RaycastManager))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private QRScanner _QRScanner;
    [SerializeField] private Transform _container;
    [SerializeField] private Panel _panelPrefab;

    private RaycastManager _raycastManager;
    private Panel _panel;

    // Subscribe
    private void OnEnable()
    {
        _QRScanner.QRDecoded += OnQRDecoded;
    }

    // Unsubscribe
    private void OnDisable()
    {
        _QRScanner.QRDecoded -= OnQRDecoded;
    }

    private void Start()
    {
        _raycastManager = GetComponent<RaycastManager>();
        _panel = null;
    }

    private void OnQRDecoded(string text)
    {
        if (_panel == null)
        {
            CreatePanel();
        }

        UpdatePanelPosition();

        try
        {
            _panel.SetInfo(cDataHolder.CreateFromString(text));
        }
        catch
        {
            _panel.Reset();
        }
    }

    // Update panel position
    private void UpdatePanelPosition()
    {
        Vector3 position = _raycastManager.TryGetRaycastPosition();
        _panel.transform.position = position;
    }

    private void CreatePanel()
    {
        int attemptsCount = 25;

        for (int i = 0; i < attemptsCount; i++)
        {
            Vector3 position = _raycastManager.TryGetRaycastPosition();
            if (position != default)
            {
                _panel = Instantiate(_panelPrefab, position, Quaternion.identity, _container);
                return;
            }
        }
    }
}
