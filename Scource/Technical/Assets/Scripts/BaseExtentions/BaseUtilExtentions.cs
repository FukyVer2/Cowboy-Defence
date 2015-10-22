using UnityEngine;
using System.Collections;

public class BaseUtilExtentions : MonoSingleton<BaseUtilExtentions> {


    public virtual float CalAngleWithTarget(Transform _objectCurrent, Vector3 _positionTarget)
    {
        Vector3 positionTarget = _positionTarget;
        Vector3 relative = positionTarget - _objectCurrent.position;//transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

        return angle;
    }

    public virtual float CalAngleWithTarget(Transform _objectCurrent, Transform _objectTarget)
    {
        Vector3 positionTarget = _objectTarget.position;
        Vector3 relative = positionTarget - _objectCurrent.position;//transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

        return angle;
    }

    public void Rotate(Transform _transform, float _angle)
    {
        Vector3 rotate = _transform.rotation.eulerAngles;
        rotate.z = _angle;
        _transform.rotation = Quaternion.Euler(rotate);
    }

    public void FlipHorizatal(ref Transform _transform, float _angle)
    {
        Vector3 rotate = _transform.rotation.eulerAngles;
        rotate.x = _angle;
        _transform.rotation = Quaternion.Euler(rotate);
    }

    public void FlipVertical(ref Transform _transform, float _angle)
    {
        Vector3 rotate = _transform.rotation.eulerAngles;
        rotate.y = _angle;
        _transform.rotation = Quaternion.Euler(rotate);
    }

    public float RoundToFloat(float value, int number)
    {
        int pow = (int)Mathf.Pow(10, number);
        int newValue = Mathf.RoundToInt(value * pow);
        return (float)newValue / pow;
        
    }
}
