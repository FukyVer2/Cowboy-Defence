using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UILevel : MonoBehaviour {
    public Text totalLevel;// tong so man choi 
    public Text currentLevel;
    public int totalLevelCount;
	// Use this for initialization
	void Start () {
        totalLevel.text = totalLevelCount.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        currentLevel.text = SpawnEnemyController.Instance.groupEnemyCurrentIndex.ToString();
	}
}
