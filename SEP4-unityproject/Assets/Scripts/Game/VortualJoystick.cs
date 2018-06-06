using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class VortualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgImg;
    private Image joystickImg;

    public Vector3 inputDirection { set; get; }

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        inputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (bgImg.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            //stopping joystick from gong out of boundaries
            inputDirection = new Vector3(x, 0, y);
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;


            //moves image
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
                inputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3)); 


        }
    }

    //moves joystick when click somewhere
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    //resets joystick on middle after letting it go
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

    }

}
