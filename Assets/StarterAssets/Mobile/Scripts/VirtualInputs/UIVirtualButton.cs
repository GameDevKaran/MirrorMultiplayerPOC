using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIVirtualButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Graphics")]
    [SerializeField] private bool isGraphicSupport;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color pressedColor;

    [Header("Output")]
    public UnityEvent<bool> buttonStateOutputEvent;

    [SerializeField] private Transform graphicObj;
    [SerializeField] private Image graphicImg;
    private bool isPressed;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isGraphicSupport)
        {
            if (!isPressed)
            {
                isPressed = true;
                graphicImg.color = pressedColor;
            }
            SetGraphicObjectPosition(Input.GetTouch(0).deltaPosition.x);
        }
        OutputButtonStateValue(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isGraphicSupport)
        {
            if (isPressed)
            {
                isPressed = false;
                graphicImg.color = normalColor;
            }
            SetGraphicObjectPosition(0);
        }

        OutputButtonStateValue(false);
    }
    void OutputButtonStateValue(bool buttonState)
    {
        buttonStateOutputEvent.Invoke(buttonState);
    }
    private void SetGraphicObjectPosition(float newPosX)
    {
        graphicObj.position = new Vector2(newPosX, graphicObj.position.y);
    }
}
