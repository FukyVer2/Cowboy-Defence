using UnityEngine;
using System.Collections;

public class UIHealth : MonoBehaviour {

    public Transform hp;
    public float hpNormal;

    void Awake()
    {
        hpNormal = hp.localScale.x;
    }

    public void UpdateHealthPoint(float percent)
    {
        hp.localScale = new Vector3(hpNormal * percent, hp.localScale.y, hp.localScale.z);
    }
}
