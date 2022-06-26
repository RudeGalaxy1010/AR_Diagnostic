using UnityEngine;

public class ClickGestureTracker : MonoBehaviour
{
    private void Update()
    {
        if (SwipeReceived())
        {
            Debug.Log("Swipe page");
        }
    }

    private bool SwipeReceived()
    {
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
        GestureInfo gestureInfo = handInfo.gesture_info;
        ManoGestureTrigger trigger = gestureInfo.mano_gesture_trigger;

        if (trigger == ManoGestureTrigger.SWIPE_LEFT)
        {
            Handheld.Vibrate();
            return true;
        }

        return false;
    }
}
