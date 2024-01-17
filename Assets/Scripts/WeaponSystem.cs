 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public Projectile m_Projectile;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class test : WeaponSystem
{
    void Start()
    {
        m_Projectile.m_damage = 100;
    }
}