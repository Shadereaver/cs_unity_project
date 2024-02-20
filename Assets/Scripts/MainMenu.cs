using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_SettingsPanel;

    public void NewGame()
    {
        LevelManager.Instance.ChangeScene(3);
        LevelManager.Instance.Load(1);
    }

    public void LoadSave()
    {

    }

    public void ToggleSettings()
    {
        m_SettingsPanel.SetActive(!m_SettingsPanel.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
