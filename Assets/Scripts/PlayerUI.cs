using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image m_PlayerHealth;
    [SerializeField] GameObject m_Settings;
    [SerializeField] GameObject m_Pause;
    [SerializeField] GameObject m_DeathScreen;

   public void HealthUpdate(float health)
    {
        m_PlayerHealth.fillAmount = health / 100;
    }

    public void TogglePause()
    {
        m_Pause.SetActive(!m_Pause.activeSelf);
    }

    public void ToggleSettings()
    {
        m_Settings.SetActive(!m_Settings.activeSelf);
    }

    public void Exit()
    {
        LevelManager.Instance.Unload(1);
        LevelManager.Instance.ChangeScene(2);
    }

    public void Dead()
    {
        m_DeathScreen.SetActive(!m_DeathScreen.activeSelf);
    }
}
