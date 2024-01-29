using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image m_PlayerHealth;
    [SerializeField] GameObject m_Settings;
    [SerializeField] GameObject m_Pause;
    [SerializeField] GameObject m_DeathScreen;

    bool m_bPaused = false;
    bool m_bShowSettings = false;
    bool m_bShowDeathScreen = false;

   public void HealthUpdate(float health)
    {
        m_PlayerHealth.fillAmount = health / 100;
    }

    public void TogglePause()
    {
        m_bPaused = !m_bPaused;
        m_Pause.SetActive(m_bPaused);
    }

    public void ToggleSettings()
    {
        m_bShowSettings = !m_bShowSettings;
        m_Settings.SetActive(m_bShowSettings);
    }

    public void Exit()
    {
        Manager.Instance.Unload(1);
        Manager.Instance.ChangeScene(2);
    }

    public void Dead()
    {
        m_bShowDeathScreen = !m_bShowDeathScreen;
        m_DeathScreen.SetActive(m_bShowDeathScreen);
    }
}
