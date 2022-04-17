using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CinemachineInputProviderOverride : MonoBehaviour/*, AxisState.IInputAxisProvider*/
{
    [SerializeField] private UnityEvent<Vector2> virtualLookInput;
    [SerializeField] private Slider senstivitySlider;
    private float senstivity = 10f;
    private float unSafeAreaSize;
    private void Start()
    {
        unSafeAreaSize = Screen.width / 4;
        senstivitySlider.value = senstivity;
    }
    private void Update()
    {
        virtualLookInput?.Invoke(GetAxisValueVector2() * senstivity);
    }

    #region Vector2
    public Vector2 GetAxisValueVector2()
    {
        if (Input.touchCount <= 0) return Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++)
        {
            var value = GetBothAxisValue(Input.GetTouch(i));
            if (value.magnitude != 0) return value;
        }
        return Vector2.zero;
    }
    public Vector2 GetBothAxisValue(Touch touch)
    {
        if (IsTouchOverMoveJoyStick(touch.position)) return Vector2.zero;
        return new Vector2(touch.deltaPosition.x, -touch.deltaPosition.y);
    }
    #endregion

    #region Float
    public float GetAxisValue(int axis)
    {
        if (Input.touchCount <= 0) return 0;
        for (int i = 0; i < Input.touchCount; i++)
        {
            float value = GetValueByAxis(Input.GetTouch(i), axis);
            if (value != 0) return value;
        }
        return 0;
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
    #endregion

    private bool IsTouchOverMoveJoyStick(Vector2 touchPosition)
    {
        return touchPosition.x <= unSafeAreaSize && touchPosition.y <= Screen.height;
    }
    public void OnSenstivityUpdate(float newValue)
    {
        senstivity = newValue;
    }
}
