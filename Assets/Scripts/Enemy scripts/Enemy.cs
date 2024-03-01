using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] Collider2D m_AttackRadius;
    [SerializeField] float m_MaxHealth;
    [SerializeField] float m_Damage;
    [SerializeField] float m_AttackSpeed;
    [SerializeField] Image m_HealthBar;

    float m_Health;
    Transform m_Target;
    NavMeshAgent m_Agent;
    bool m_CoolDown = false;


    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_Health = m_MaxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        m_Target = (m_Target == null && collision.gameObject.GetComponent<Player>() != null) ? collision.gameObject.GetComponent<Player>().transform : m_Target;

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

            if (collision.GetComponent<IDamage>() != null)
            {
                collision.GetComponent<IDamage>().Damage(m_Damage);
            }
        }
    }

    public void Damage(float damage)
    {
        SoundManager.PlaySound(Sounds.EnemyHurt);

        m_Health -= damage;

        m_HealthBar.fillAmount = m_Health / m_MaxHealth;

        if (m_Health <= 0) 
        {
            Destroy(gameObject);
        }

        if (m_Health > m_MaxHealth)
        {
            m_Health = m_MaxHealth;
        }
    }

    IEnumerator CoolDown()
    {
        m_CoolDown = true;
        yield return new WaitForSeconds(m_AttackSpeed);
        m_CoolDown = false;
    }
}
