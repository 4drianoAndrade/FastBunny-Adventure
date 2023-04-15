using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchButtons : MonoBehaviour
{
    public GameObject objectAction;
    public actions buttonActionMoveRight;
    public actions buttonActionStopRight;
    public actions buttonActionMoveLeft;
    public actions buttonActionStopLeft;
    public actions buttonActionJump;

    public void OnPointerPressedMoveRight()
    {
        objectAction.SendMessage(buttonActionMoveRight.ToString(), SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerStopMoveRight()
    {
        objectAction.SendMessage(buttonActionStopRight.ToString(), SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerPressedMoveLeft()
    {
        objectAction.SendMessage(buttonActionMoveLeft.ToString(), SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerStopMoveLeft()
    {
        objectAction.SendMessage(buttonActionStopLeft.ToString(), SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerDownJump()
    {
        objectAction.SendMessage(buttonActionJump.ToString(), SendMessageOptions.DontRequireReceiver);
    }
}
