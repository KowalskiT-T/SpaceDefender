using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    static ScoreKeeper instance;
    float score = 0;
    void Awake()
    {
        ManageSingelton();
    }

    void ManageSingelton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public int GetScore()
    {
        return (int)score;
    }

}
