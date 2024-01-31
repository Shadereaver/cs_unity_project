using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>()?.EnterSafe();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Player>()?.ExitSafe();
    }
}
