using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public UnityEvent ScoreUpdate;

    public void AddScore(int score)
    {
        this.score += score;
        ScoreUpdate.Invoke();
    }
}
