using UnityEngine;
using System.Collections;

public class BaseUtilExtentions : MonoSingleton<BaseUtilExtentions> {

    public void Rotate(ref Transform _transform, float _angle)
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
}
