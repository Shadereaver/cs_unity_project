using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Transform m_Spawn;
    bool m_bSceneChanged = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateSpawnLocation()
    {
        m_bSceneChanged = true;
        if (GameObject.FindGameObjectWithTag("Respawn") != null)
            m_Spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        MovePlayerToSpawn();
    }

    public void MovePlayerToSpawn()
    {
        if (FindObjectOfType<Player>() && m_bSceneChanged)
        {
            FindObjectOfType<Player>().transform.position = m_Spawn.position;
            m_bSceneChanged = false;
        }
    }
}
