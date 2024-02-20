using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.ChangeScene(4);
    }
}
