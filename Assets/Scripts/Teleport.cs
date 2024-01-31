using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Manager.Instance.ChangeScene(4);
    }
}
