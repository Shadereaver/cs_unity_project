using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Stuff
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    private Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    [Header("Movement parameters")]
    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;
    #endregion

    [SerializeField] float m_Health;



    [SerializeField] float m_AttackSpeed;
    [SerializeField] int m_ClipSize;
    [SerializeField] Rigidbody2D m_BulletPrefab;
    [SerializeField] Transform m_FirePoint;
    [SerializeField] float m_BulletSpeed;

    float m_AttackTimer;
    int m_Bullets;
    Vector3 m_WorldMousePos;


    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_AttackTimer = 0;
        m_Bullets = m_ClipSize;
    }

    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = playerDirection.normalized * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
    }

    private void Update()
    {
        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

        m_WorldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_WorldMousePos.z = 0;

        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (playerDirection.magnitude != 0)
        {
            animator.SetFloat("Horizontal", playerDirection.x);
            animator.SetFloat("Vertical", playerDirection.y);
            animator.SetFloat("Speed", playerDirection.magnitude);

            //And set the speed to 1, so they move!
            playerSpeed = 1f;
        }

        else
        {
            //Was the input just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }

        // Was the fire button pressed (mapped to Left mouse button or gamepad trigger)
        if (Input.GetButtonDown("Fire2"))
        {
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_AttackTimer <= 0f)
            {
                fire();
                --m_Bullets;

                if (m_Bullets <= 0)
                {
                    m_Bullets = m_ClipSize;
                    m_AttackTimer = 5;
                }
                else
                {
                    m_AttackTimer = m_AttackSpeed;
                }
            }
        }

        m_AttackTimer -= Time.deltaTime;
    }

    public void damage(float damage)
    {
        Debug.Log($"Damage: {damage}");

        m_Health -= damage;

        if (m_Health <= 0f)
            Debug.Log("Dead");
    }

    void fire()
    {
        Instantiate(m_BulletPrefab, transform.position, Quaternion.identity).AddForce(m_WorldMousePos * m_BulletSpeed, ForceMode2D.Impulse);
    }
}
