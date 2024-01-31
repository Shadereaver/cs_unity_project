using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform m_Target;
    NavMeshAgent m_Agent;

    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        m_Target = collision.gameObject.GetComponent<Player>()?.transform;
    }

    void Update()
    {
        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.position);
        }
    }
}
