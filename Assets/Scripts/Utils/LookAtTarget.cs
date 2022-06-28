using UnityEngine;

// Helpping class for looking at target
public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    // Update rotation with look at target each frame
    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        transform.LookAt(transform.position + _target.forward);
    }
}
