using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [SerializeField] GameObject m_Explosion;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    public GameObject SpawnExplosion(Vector3 position, float lifeSpan = 0.5f)
    {
        GameObject ExplosionRef = Instantiate(m_Explosion, position, Quaternion.identity);

        Destroy(ExplosionRef, lifeSpan);

        return ExplosionRef;
    }
}
