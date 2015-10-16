using UnityEngine;
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
        GameController.Instance.playerGun.SpawnOfBullet();
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
