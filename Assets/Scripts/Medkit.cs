using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.PlaySound(Sounds.Pickup);

            collision.GetComponent<IDamage>().Damage(Random.Range(-50, -10));
            Destroy(gameObject);
        }
    }
}
