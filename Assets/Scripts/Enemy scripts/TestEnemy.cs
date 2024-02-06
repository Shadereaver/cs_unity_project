using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Collider2D m_AttackRadius;

    Transform m_Target;
    NavMeshAgent m_Agent;
    bool m_CoolDown = false;


    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        m_Target = (m_Target == null) ? collision.gameObject.GetComponent<Player>()?.transform : m_Target;

        Attack(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Attack(collision);
    }

    void Update()
    {
        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.position);
        }
    }

    void Attack(Collider2D collision)
    {
        if (m_AttackRadius.IsTouching(collision) && !m_CoolDown)
        {
            StartCoroutine(CoolDown());

            collision.GetComponent<IDamage>()?.Damage(10);
        }
    }

    IEnumerator CoolDown()
    {
        m_CoolDown = true;
        yield return new WaitForSeconds(1);
        m_CoolDown = false;
    }
}
