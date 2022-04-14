using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CinemachineInputProviderOverride : MonoBehaviour, AxisState.IInputAxisProvider
{
    private float unSafeAreaSize;
    private void Start()
    {
        unSafeAreaSize = Screen.width / 4;
        Debug.Log($"unSafeAreaSize: {Screen.width / 4}");
    }
    public float GetAxisValue(int axis)
    {
        if (Input.touchCount <= 0) return 0;
        for (int i = 0; i < Input.touchCount; i++)
        {
            float value = GetValueByAxis(Input.GetTouch(i), axis);
            if (value != 0) return value;
        }
        return 0;
        //return GetValueByAxis(Input.GetTouch(Input.touchCount - 1), axis);
    }
    private float GetValueByAxis(Touch touch, int axis)
    {
        if (IsTouchOverMoveJoyStick(touch.position)) return 0;
        return axis switch
        {
            0 => touch.deltaPosition.x,
            1 => touch.deltaPosition.y,
            _ => 0,
        };
    }
    private bool IsTouchOverMoveJoyStick(Vector2 touchPosition)
    {
        return touchPosition.x <= unSafeAreaSize && touchPosition.y <= unSafeAreaSize;
    }
}
