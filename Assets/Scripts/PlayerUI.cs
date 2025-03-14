using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image m_PlayerHealth;
    [SerializeField] GameObject m_Settings;
    [SerializeField] GameObject m_Pause;
    [SerializeField] GameObject m_DeathScreen;

    void Awake()
    {
        transform.Find("Pause/Settings/Toggle").GetComponent<Toggle>().onValueChanged.AddListener(SoundManager.ToggleBGM);
    }

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
        LevelManager.Unload();
        LevelManager.ChangeScene(2);
    }

    public void Dead()
    {
        m_DeathScreen.SetActive(!m_DeathScreen.activeSelf);
    }
}
