using UnityEngine;
using UnityEngine.Events;

// Class for detecting gesture input
public class GestureInput : MonoBehaviour
{
    public event UnityAction RightSwipeReceived;
    public event UnityAction LeftSwipeReceived;

    // Check if swipe received each frame
    private void Update()
    {
        ReceiveSwipes();
    }

    // Method detects swipe gestures and call action
    private void ReceiveSwipes()
    {
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
        GestureInfo gestureInfo = handInfo.gesture_info;
        ManoGestureTrigger trigger = gestureInfo.mano_gesture_trigger;

        if (trigger == ManoGestureTrigger.SWIPE_RIGHT)
        {
            RightSwipeReceived?.Invoke();
        }
        else if (trigger == ManoGestureTrigger.SWIPE_LEFT)
        {
            LeftSwipeReceived?.Invoke();
        }
    }
}
