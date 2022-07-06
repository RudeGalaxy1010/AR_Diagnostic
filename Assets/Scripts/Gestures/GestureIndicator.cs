using UnityEngine;

[RequireComponent(typeof(GestureInput))]
public class GestureIndicator : MonoBehaviour
{
    private GestureInput _gestureInput;

    private void Awake()
    {
        _gestureInput = GetComponent<GestureInput>();
    }

    private void OnEnable()
    {
        _gestureInput.RightSwipeReceived += Indicate;
        _gestureInput.LeftSwipeReceived += Indicate;
    }

    private void OnDisable()
    {
        _gestureInput.RightSwipeReceived -= Indicate;
        _gestureInput.LeftSwipeReceived -= Indicate;
    }

    private void Indicate()
    {
        Handheld.Vibrate();
    }
}
