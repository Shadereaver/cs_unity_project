using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_SettingsPanel;

    void Awake()
    {
       m_SettingsPanel.transform.Find("Toggle").GetComponent<Toggle>().onValueChanged.AddListener(SoundManager.ToggleBGM);
    }

    public void NewGame()
    {
        LevelManager.LoadPlayer();
        LevelManager.ChangeScene(3);
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
