using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Canvas m_WinScreen;
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(end());
    }

    IEnumerator end()
    {
        m_WinScreen.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1.0f;
        m_WinScreen.gameObject.SetActive(false);

        LevelManager.ChangeScene(2);
        LevelManager.Unload();
    }
}
