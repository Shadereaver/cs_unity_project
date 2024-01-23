using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    ScoreSystem m_ScoreSystem;
    public TMPro.TextMeshProUGUI uiLable;

    void Awake()
    {
        m_ScoreSystem = GetComponent<ScoreSystem>();
    }

    public void changeScore()
    {
        uiLable.text = "Score: " + m_ScoreSystem.score;
    }
}
