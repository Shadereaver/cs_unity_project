using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_StartPoint;
    [SerializeField] Transform m_EndPoint;
    [SerializeField] int m_MoveSpeed;

    Transform m_Target;

    // Start is called before the first frame update
    void Start()
    {
        m_Target = m_StartPoint;
    }

    // Update is called once per frame
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
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("damage", 10);
    }

    void ChangeTarget()
    {
        m_MoveSpeed *= -1;
    }
}
