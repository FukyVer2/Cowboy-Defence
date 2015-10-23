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
#if UNITY_EDITOR
        Debug.Log("Toa do diem click" + Camera.main.ScreenToWorldPoint(eventData.position));
#endif
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
        GameController.Instance.player.GunShoot(newPosition);
    }

    void Update()
    {
        if (oldEventData != null && oldEventData.IsPointerMoving())
        {
            if (GameController.Instance.player.IsAutoGun() && isPress)
            {
                //isPress = false;
                //oldEventData = null;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(oldEventData.position);
                Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
                GameController.Instance.player.GunShoot(newPosition);
            }
            else
            {
                oldEventData = null;
                isPress = false;
            }
            //GameController.Instance.player.GunStop();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        GameController.Instance.player.GunStop();
    }
}
