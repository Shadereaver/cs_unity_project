using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_SettingsPanel;
    bool m_ShowSettings = false;

    public void NewGame()
    {
        Manager.Instance.ChangeScene(3);
        Manager.Instance.Load(1);
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
}
