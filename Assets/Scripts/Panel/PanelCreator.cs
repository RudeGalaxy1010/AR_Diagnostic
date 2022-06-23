using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PanelCreator : MonoBehaviour
{
    private const float RayCastDistance = 15f;

    [SerializeField] private Camera _camera;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private UIPanel _panelPrefab;
    [SerializeField] private Transform _container;

    public UIPanel TryCreatePanel()
    {
        Vector3 screenCenter = _camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        ARRaycast raycast = _raycastManager.AddRaycast(screenCenter, RayCastDistance);

        if (raycast != null)
        {
            Vector3 position = raycast.transform.position;
            UIPanel panel = Instantiate(_panelPrefab, position, Quaternion.identity, _container);

            if (panel.TryGetComponent(out LookAtTarget lookAtTarget))
            {
                lookAtTarget.SetTarget(_camera.transform);
            }

            return panel;
        }

        return null;
    }
}
