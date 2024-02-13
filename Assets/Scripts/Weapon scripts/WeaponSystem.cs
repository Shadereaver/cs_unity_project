using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] protected float m_Damage;
    [SerializeField] protected float m_Range;
    [SerializeField] protected float m_AttackSpeed;
    [SerializeField] protected LayerMask m_Host;

    protected Vector3 m_MouseWorldPos;
    protected bool m_bCoolDown;

    void Awake()
    {
        IWeaponSystem m_MainScript = gameObject.GetComponentInParent<IWeaponSystem>();
        if (m_MainScript == null)
            throw new UnityException("No Weapon interface");

        else
        {
            m_MainScript.PrimaryFire.AddListener(PrimaryFire);
            m_MainScript.SecondaryFire.AddListener(SecondaryFire);
        }
    }

    void Update()
    {
        m_MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MouseWorldPos.z = 0;
    }

    protected virtual void PrimaryFire()
    {
    }

    protected virtual void SecondaryFire()
    {
    }

    protected IEnumerator CoolDown()
    {
        m_bCoolDown = true;
        yield return new WaitForSeconds(m_AttackSpeed);
        m_bCoolDown = false;
    }
}

public class ProjectileWeaponSystem : WeaponSystem
{
    [SerializeField] protected Projectile m_Projectile;
    [SerializeField] protected float m_ProjectileSpeed;

    void Start()
    {
        m_Projectile.m_Damage = m_Damage;
        m_Projectile.GetComponent<Collider2D>().excludeLayers = m_Host;
    }

    protected override void PrimaryFire()
    {
        if (!m_bCoolDown)
        {
            Projectile projectile = Instantiate(m_Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce((m_MouseWorldPos - transform.position).normalized * m_ProjectileSpeed, ForceMode2D.Impulse);
            StartCoroutine(CoolDown());
        }

    }
}

public interface IWeaponSystem
{
    UnityEvent PrimaryFire {get;}
    UnityEvent SecondaryFire{get;}
}

public interface IDamage
{
    void Damage(float damage);
}
