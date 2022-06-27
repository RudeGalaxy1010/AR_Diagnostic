using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RaycastManager : MonoBehaviour
{
    private const float RayCastDistance = 15f;

    [SerializeField] private Camera _camera;
    [SerializeField] private ARRaycastManager _raycastManager;

    public Vector3 TryGetRaycastPosition(float screenPointX = 0.5f, float screenPointY = 0.5f)
    {
        Vector3 screenCenter = _camera.ViewportToScreenPoint(new Vector2(screenPointX, screenPointY));
        ARRaycast raycast = _raycastManager.AddRaycast(screenCenter, RayCastDistance);

        if (raycast != null)
        {
            return raycast.transform.position;
        }

        return default;
    }
}
