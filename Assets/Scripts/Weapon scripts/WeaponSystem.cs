using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[Serializable] struct PrimaryWeaponInfo
{
    public float Damage;
    public float AttackSpeed;
    public float ProjectileSpeed;
    public Projectile Projectile;
    public int MagSize;
    public float ReloadTime;
}

[Serializable] struct SecondaryWeaponInfo
{
    public float Damage;
    public float AttackSpeed;
    public float Range;
    public Animator Animator;
}

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] PrimaryWeaponInfo m_PrimaryWeaponInfo;
    [SerializeField] SecondaryWeaponInfo m_SecondaryWeaponInfo;

    [SerializeField] LayerMask m_Host;

    [SerializeField] TextMeshProUGUI TotalAmmoCountText;
    [SerializeField] int m_TotalAmmoCount;
    public int TotalAmmo
    {
        get => m_TotalAmmoCount;

        set
        {
            m_TotalAmmoCount = value;
            TotalAmmoCountText.text = "Total ammo: " + m_TotalAmmoCount;
        }
    }

    [SerializeField] TextMeshProUGUI MagAmmoCountText;
    int m_AmmoInMagValue;
    int AmmoInMag
    {
        get => m_AmmoInMagValue;

        set
        {
            m_AmmoInMagValue = value;
            MagAmmoCountText.text = "Ammo: " + value + "/" + m_PrimaryWeaponInfo.MagSize;
        }
    }
    
    Vector3 m_MouseWorldPos;
    bool m_bReloading = false;
    bool m_bPrimaryCoolDown = false;
    bool m_bSecondaryCoolDownValue = false;
    bool SecondaryCoolDown 
        {
            get => m_bSecondaryCoolDownValue;
        
            set
            {
                m_bSecondaryCoolDownValue = value;
                m_SecondaryWeaponInfo.Animator.SetBool("Swing", value);
            }
        }

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

    void Start()
    {
        m_PrimaryWeaponInfo.Projectile.Damage = m_PrimaryWeaponInfo.Damage;
        m_PrimaryWeaponInfo.Projectile.GetComponent<Collider2D>().excludeLayers = m_Host;
        
        GetComponent<CircleCollider2D>().radius = m_SecondaryWeaponInfo.Range;
        GetComponent<CircleCollider2D>().excludeLayers = m_Host;

        AmmoInMag = m_PrimaryWeaponInfo.MagSize;
    }

    void Update()
    {
        m_MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MouseWorldPos.z = 0;

        transform.up = ((Vector2)m_MouseWorldPos - (Vector2)transform.position).normalized;
    }

    void PrimaryFire()
    {
        if (AmmoInMag <= 0 && TotalAmmo != 0 && !m_bReloading)
        {
            StartCoroutine(Reload());
        }
        
        else if (!m_bPrimaryCoolDown && AmmoInMag != 0)
        {
            SoundManager.PlaySound(Sounds.GunShoot);
            Projectile projectile = Instantiate(m_PrimaryWeaponInfo.Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>()
                .AddForce((m_MouseWorldPos - transform.position).normalized * m_PrimaryWeaponInfo.ProjectileSpeed, ForceMode2D.Impulse);

            -- AmmoInMag;
            StartCoroutine(CoolDown(result => m_bPrimaryCoolDown = result, m_PrimaryWeaponInfo.AttackSpeed));
        }
    }

    void SecondaryFire()
    {
        if (!SecondaryCoolDown)
        {
            SoundManager.PlaySound(Sounds.SwordSwipe);
            List<Collider2D> colliders = new();
            ContactFilter2D filter2D = new();
            GetComponent<Collider2D>().OverlapCollider(filter2D.NoFilter(), colliders);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<IDamage>() != null && collider.gameObject.layer != math.log2(m_Host.value) && !collider.isTrigger)
                    collider.GetComponent<IDamage>().Damage(m_SecondaryWeaponInfo.Damage);
            }

            StartCoroutine(CoolDown(result => SecondaryCoolDown = result, m_SecondaryWeaponInfo.AttackSpeed));
        }
    }

    IEnumerator CoolDown(Action<bool> bCoolDown, float attackspeed)
    {
        bCoolDown(true);
        yield return new WaitForSeconds(attackspeed);
        bCoolDown(false);
    }

    IEnumerator Reload()
    {
        m_bReloading = true;
        yield return new WaitForSeconds(m_PrimaryWeaponInfo.ReloadTime);
        m_bReloading = false;

        TotalAmmo -= m_PrimaryWeaponInfo.MagSize;
        AmmoInMag = m_PrimaryWeaponInfo.MagSize;
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
