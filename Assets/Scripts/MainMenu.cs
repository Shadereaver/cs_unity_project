using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_SettingsPanel;
    [SerializeField] GameObject m_LoadScreen;
    bool m_ShowSettings = false;

    public void NewGame()
    {
        m_LoadScreen.SetActive(true);
        StartCoroutine(AsyncLoad(1));
    }

    public void LoadSave()
    {

    }

    public void ToggleSettings()
    {
        m_ShowSettings = !m_ShowSettings;
        m_SettingsPanel.SetActive(m_ShowSettings);
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator AsyncLoad(int SceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
