using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;

    void Reset()
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
            gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;

        if (gameObject.GetComponent<CircleCollider2D>() == null)
            gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IDamage>() != null)
        {
            collision.gameObject.GetComponent<IDamage>().Damage(Damage);
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
