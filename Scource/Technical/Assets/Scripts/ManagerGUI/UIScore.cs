using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {

    public Text scoreView; //Diem hien thi tren man hinh
    public float totalScore; //Diem hien thi
    private float currentScore; //So diem hien tai
    public float timeInscrease; //Thoi gian xay ra hieu ung tang diem, tang len mot con so mat bao nhieu day
    public float timeCurrent;
    bool isIncreaseScore;

    void Start()
    {
        isIncreaseScore = false;
    }

    public void UpdateScoreView(float _score)
    {
        totalScore += _score;
        //scoreView.text = _score.ToString();
        isIncreaseScore = true;
    }

    public void IncreaseScore()
    {
        if (currentScore == totalScore)
        {
            isIncreaseScore = false;
        }
        currentScore++;
        scoreView.text = currentScore.ToString();
    }

    void Update()
    {
        if (isIncreaseScore)
        {
            timeCurrent += Time.deltaTime;
            if (timeCurrent >= timeInscrease)
            {
                IncreaseScore();
                timeCurrent = 0.0f;
            }
        }
    }
}

