using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeOfGun : MonoBehaviour {

    public List<BaseEnemyObject> enemyInBoxs;

    void Start()
    {
        enemyInBoxs = new List<BaseEnemyObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            BaseEnemyObject baseEnemy = enemy.GetComponent<BaseEnemyObject>();
            enemyInBoxs.Add(baseEnemy);
        }
    }
}
