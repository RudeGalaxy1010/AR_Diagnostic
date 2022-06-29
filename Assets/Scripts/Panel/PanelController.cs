using System;
using System.Collections;
using UnityEngine;

// Class for updating panel with actual info decoded from QR
[RequireComponent(typeof(RaycastManager))]
public class PanelController : MonoBehaviour
{
    private const float ScanningSessionsDelay = 0.5f;

    [SerializeField] private QRScanner _QRScanner;
    [SerializeField] private Transform _container;
    [SerializeField] private Panel _panelPrefab;

    private RaycastManager _raycastManager;
    private Panel _panel;

    private void Start()
    {
        _raycastManager = GetComponent<RaycastManager>();
        _panel = null;
        StartCoroutine(Scan());
    }

    // Try decode camera view
    private IEnumerator Scan()
    {
        while (true)
        {
            string text = _QRScanner.DecodeScreenshot();
            UpdatePanelPosition();

            try
            {
                _panel.SetInfo(cDataHolder.CreateFromString(text));
            }
            catch (Exception e)
            {

            }

            yield return new WaitForSeconds(ScanningSessionsDelay);
        }
    }

    // Update panel position or creates panel if null
    private void UpdatePanelPosition()
    {
        Vector3 position = _raycastManager.TryGetRaycastPosition();

        if (_panel == null)
        {
            _panel = Instantiate(_panelPrefab, position, Quaternion.identity, _container);
        }
        else
        {
            _panel.transform.position = position;
        }
    }
}
