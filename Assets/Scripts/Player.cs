using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;


public class Player : MonoBehaviour, IWeaponSystem, IDamage
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

    [SerializeField] PlayerUI m_PlayerUI;
    [SerializeField] float m_Health;
    [SerializeField] WeaponSystem m_WeaponPrefab;
    [SerializeField] Light2D m_Light;

    GameObject m_WeaponRef;
    bool m_bPaused;
    bool m_bWeaponSafty;
    
    public UnityEvent PrimaryFire  {get;} = new UnityEvent();
    public UnityEvent SecondaryFire {get;} = new UnityEvent();

    void Awake()
    {
        Time.timeScale = 1f;

        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        m_bPaused = false;
        m_bWeaponSafty = false;
        m_WeaponRef = Instantiate(m_WeaponPrefab, transform).gameObject;
    }

    void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime * playerDirection.normalized;
    }

    void Update()
    {
        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

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
            if (!m_bWeaponSafty)
            {
                SecondaryFire.Invoke();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!m_bWeaponSafty)
            {
                PrimaryFire.Invoke();
            }
        }

        

        if (Input.GetButtonDown("Pause"))
        {
            m_PlayerUI.TogglePause();
            Pause(); 
        }
    }

    public void Damage(float damage)
    {
        m_Health -= damage;

        if (m_Health <= 0f)
        { 
            GetComponent<SpriteRenderer>().enabled = false;
            VFXManager.Instance.SpawnExplosion(transform.position);
            m_PlayerUI.Dead();
            StartCoroutine(DeathReset());
        }

        m_PlayerUI.HealthUpdate(m_Health);
    }

    public void Pause()
    {
        m_bPaused = !m_bPaused;
        Time.timeScale = m_bPaused ? 0f : 1f;
    }

    public void EnterSafe()
    {
        if (m_WeaponRef != null)
            Destroy(m_WeaponRef);
     
        transform.position = new Vector3(0,0,0);
        m_bWeaponSafty = true;
        m_PlayerUI.transform.Find("UIHider").gameObject.SetActive(false);
        m_Light.gameObject.SetActive(false);
    }

    public void ExitSafe()
    {
        m_WeaponRef = Instantiate(m_WeaponPrefab, transform).gameObject;

        transform.position = new Vector3(0,0,0);
        m_PlayerUI.transform.Find("UIHider").gameObject.SetActive(true);
        m_bWeaponSafty = false;
        m_Light.gameObject.SetActive(true);
    }

    IEnumerator DeathReset()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        GetComponent<SpriteRenderer>().enabled = true;

        Manager.Instance.ChangeScene(3);
    }
}
