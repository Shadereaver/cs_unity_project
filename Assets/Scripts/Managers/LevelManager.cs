using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] GameObject m_LoadScreen;
    [SerializeField] GameObject m_DebugMenu;
    [SerializeField] Image m_LoadBar;
    [SerializeField] TextMeshProUGUI m_LoadPercent;

    int m_CurrentSceneIndex;

    public UnityEvent SceneChanged;
    public UnityEvent PlayerLoaded;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_LoadPercent.text = "0%";
        m_LoadBar.fillAmount = 0;
        m_CurrentSceneIndex = 2;
        StartCoroutine(AsyncLoad(2, SetActiveAndComplete));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            m_DebugMenu.SetActive(!m_DebugMenu.activeSelf);
    }

    public static void LoadPlayer() => Instance.StartCoroutine(Instance.AsyncLoad(1, (AsyncOperation operation) => { Instance.PlayerLoaded.Invoke(); }));

    public static void Unload() => Instance.StartCoroutine(Instance.AsyncUnLoad(SceneManager.GetSceneByBuildIndex(1)));

    public static void ChangeScene(int id)
    {
        Instance.StartCoroutine(Instance.AsyncUnLoad(SceneManager.GetActiveScene()));
        Instance.m_CurrentSceneIndex = id;
        Instance.StartCoroutine(Instance.AsyncLoad(id, Instance.SetActiveAndComplete));
    }

    void SetActiveAndComplete(AsyncOperation operation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(m_CurrentSceneIndex));
        if (m_CurrentSceneIndex != 2)
            SceneChanged.Invoke();
        m_LoadScreen.SetActive(false);
        m_LoadBar.fillAmount = 0;
        m_LoadPercent.text = "0%";       
    }

    public void DebugChangeScene(int scene)
    {
        scene += 2;

        if (scene == m_CurrentSceneIndex)
            return;

        if (m_CurrentSceneIndex == 2)
        {
            ChangeScene(scene);
            LoadPlayer();
        }

        else if (scene == 2)
        {
            Unload();
            ChangeScene(scene);
        }

        else
        {
            ChangeScene(scene);
        }
    }

    IEnumerator AsyncLoad(int SceneIndex, Action<AsyncOperation> func)
    {
        m_LoadScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Additive);

        asyncLoad.allowSceneActivation = false;
        asyncLoad.completed += func;

        do
        {
            m_LoadBar.fillAmount = asyncLoad.progress;
            m_LoadPercent.text = $"{(int)(asyncLoad.progress * 100)}%";
            yield return null;
        } while (asyncLoad.progress < 0.9f);
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator AsyncUnLoad(Scene SceneIndex)
    {
        AsyncOperation asyncUnLoad = SceneManager.UnloadSceneAsync(SceneIndex);

        while (!asyncUnLoad.isDone)
        {
            yield return null;
        }
    }
}
