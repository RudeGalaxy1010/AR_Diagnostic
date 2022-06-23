using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        transform.LookAt(transform.position + _target.forward);
    }
}
