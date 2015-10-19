﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputController : MonoSingleton<InputController>, IPointerDownHandler, IPointerUpHandler {

    public bool isPress;
    public Vector3 eventPosition;
    private PointerEventData oldEventData;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        eventPosition = eventData.pressPosition;
        oldEventData = eventData;
#if UNITY_EDITOR
        Debug.Log("Toa do diem click" + Camera.main.ScreenToWorldPoint(eventData.position));
#endif
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
        GameController.Instance.playerGun.GunShoot(newPosition);
    }

    void Update()
    {
        if (oldEventData != null && oldEventData.IsPointerMoving())
        {
            isPress = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }
}
