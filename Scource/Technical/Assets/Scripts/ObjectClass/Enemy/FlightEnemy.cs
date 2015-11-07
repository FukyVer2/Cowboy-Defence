using UnityEngine;
using System.Collections;

public class FlightEnemy : MonoBehaviour {
    public float a_x;
    public float a_y;
    public float v_x;
    public float v_y;

    public float maxPositionX =0.0f;
    public bool isMove;
    private bool isReleaseItem;
    private Vector3 positionBegin;

    public float timeReleaseItem;
    public float timeDelay=0;

    public GameObject obj_Items;
    public void Move()
    {
        v_x += a_x * Time.deltaTime;
        v_y += a_y * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x += v_x * Time.deltaTime;
        newPos.y += v_y * Time.deltaTime;

        if(newPos.x >=maxPositionX)
        {
            ResetOfValue();
        }
        else
        {
            transform.position = newPos;
        }
    }
    
    public void ResetOfValue()
    {
        transform.position = positionBegin;
        isMove = false;
        isReleaseItem = true;
        timeDelay = 0.0f;
    }
    void Start()
    {
        isReleaseItem = true;
        positionBegin = transform.position;
        StartCoroutine(WaitingMove(10f));
    }

    float timer=0;
    IEnumerator WaitingMove(float timeWait)
    {
        while (timer < 5)
        {
            timer++;
            yield return new WaitForSeconds(timeWait);
            timeWait = Random.Range(20,30);
            isMove = true;
        }
    }

    void Update()
    {
        if(isMove)
        {
            Move();
            if(isReleaseItem &&(timeDelay+=Time.deltaTime) >= timeReleaseItem)
            {
#if UNITY_EDITOR
                //GameObject objItems = Instantiate(obj_Items) as GameObject;
                //objItems.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y -0.4f, transform.position.z);
#endif
                ManagerObject.Instance.SpawnItem(BaseObjectType.OBB_BOM_ITEM, new Vector3(transform.position.x + 1f, transform.position.y - 0.5f, 1));
                isReleaseItem = false;
            }
        }
        
    }
}
