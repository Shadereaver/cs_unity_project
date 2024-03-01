using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.PlaySound(Sounds.Pickup);

            collision.GetComponent<Player>().AddAmmo(Mathf.RoundToInt(Random.Range(3, 10)));
            Destroy(gameObject);
        }
    }
}
