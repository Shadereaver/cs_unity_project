using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_damage;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
