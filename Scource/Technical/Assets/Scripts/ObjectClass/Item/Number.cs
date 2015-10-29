using UnityEngine;
using System.Collections;

public class Number : BaseObject {

    public SpriteRenderer[] numberSpriteRenderers;
    public string number;
    public Vector3 scaleNormal;

    void Awake()
    {
        scaleNormal = transform.localScale;
    }
    public override void InitObject()
    {
        //this.gameObjectType = BaseObjectType.OU_NUMBER;
        //positionBegin = transform.position;
    }

    public void Reset()
    {
        transform.localScale = scaleNormal;
        positionBegin = transform.position;
    }

    public void SetValue(string _number)
    {
        int count = 0;
        SpriteRenderer spriteRender;
        foreach (var item in _number)
        {
            numberSpriteRenderers[count++].sprite = ResourceManager.Instance.GetSpriteByName("number_" + item);
        }
        while (count < numberSpriteRenderers.Length)
        {
            spriteRender = numberSpriteRenderers[count++];
            spriteRender.enabled = false;
        }
        NumberAction();
        //Number di chuyen
    }

    public void NumberAction()
    {
        Vector3 positionMove = positionBegin;
        positionMove.y += 5;
        iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.time, 1.0f,
                                            iT.MoveTo.islocal, true,
                                            iT.MoveTo.position, positionMove,
                                            iT.MoveTo.easetype, iTween.EaseType.linear));
        //
        iTween.ScaleTo(gameObject, iTween.Hash(iT.ScaleTo.time, 1.0f,
                                         iT.ScaleTo.scale, new Vector3(0.4f, 0.4f, 0.4f),
                                         iT.ScaleTo.easetype, iTween.EaseType.linear,
                                         iT.ScaleTo.oncomplete, "OnComplete"));
    }

    void OnComplete()
    {
        PoolCustomize.Instance.HideBaseObject(gameObject, "Item");
    }

    public override void UpdateObject()
    {
       
    }

    public override void DestroyObject()
    {
        
    }
}
