using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_StartPoint;
    [SerializeField] Transform m_EndPoint;
    [SerializeField] int m_MoveSpeed;
    [SerializeField] int m_Damage;

    Transform m_Target;

    void Start()
    {
        m_Target = m_StartPoint;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingObstacleWaypoint"))
            ChangeTarget();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<IDamage>()?.Damage(m_Damage);
    }

    void ChangeTarget()
    {
        m_MoveSpeed *= -1;
    }
}
