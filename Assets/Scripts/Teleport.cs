using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            collision.GetComponent<Player>().ExitHub();
        }
        catch
        {
            throw new UnityException("No player script");
        }

        Manager.Instance.ChangeScene(4);
    }
}
