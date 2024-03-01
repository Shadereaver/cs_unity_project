using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField] GameObject m_Goal;
    void OnDestroy()
    {
        m_Goal.SetActive(true);
    }
}
