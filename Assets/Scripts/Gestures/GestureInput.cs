using UnityEngine;
using UnityEngine.Events;

public class GestureInput : MonoBehaviour
{
    public event UnityAction RightSwipeReceived;
    public event UnityAction LeftSwipeReceived;

    private void Update()
    {
        ReceiveSwipes();
    }

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
