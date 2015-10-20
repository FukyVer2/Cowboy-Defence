using UnityEngine;
using System.Collections;

public abstract class BaseObject : MonoBehaviour {

    protected string objectName; //Ten object
    public Vector3 positionBegin; //Vi tri ban dau cua object
    public BaseObjectType gameObjectType; 

    /// <summary>
    /// Get the name of object
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return objectName;
    }

    /// <summary>
    /// Set the name of object
    /// </summary>
    /// <param name="_objectName"></param>
    public void SetName(string _objectName)
    {
        this.objectName = _objectName;
    }

	void Start () {
        this.InitObject();
	}
	
	
	void Update () {
        this.UpdateObject();
        //this.DestroyObject();
	}

    /// <summary>
    /// Init GamebOject
    /// </summary>
    public abstract void InitObject();

    /// <summary>
    /// Update GameObject
    /// </summary>
    public abstract void UpdateObject();

    /// <summary>
    /// Destroy GameObject
    /// </summary>
    public abstract void DestroyObject();

}
