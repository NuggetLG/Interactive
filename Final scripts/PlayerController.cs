using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //Singleton
    public static PlayerController instance;


    //Modulos
    InputManager inputManager;
    PlayerJump playerJump;
    PlayerRun playerRun;
    PlayerDash playerDash;
    PlayerHit hit;

    //Referencias
    Rigidbody rb;
    Animator anim;
    CapsuleCollider playerCollider;

    //CoyoteTime
    public bool falling;
    float coyoteTime = 0.2f;
    float coyoteTimer;

    bool canHit;

    public bool DisableJump;
    public bool DisableDash;
    public bool DisableHit;

    public InputManager InputManager { get => inputManager; set => inputManager = value; }

    public event Action playerFail;
    public event Action playerPunch;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerJump = GetComponent<PlayerJump>();
        playerRun = GetComponent<PlayerRun>();
        playerDash = GetComponent<PlayerDash>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hit = GetComponent<PlayerHit>();

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ActiveJump()
    {
        DisableJump = false;
        playerJump.canJump = true;
    }

    public void ActiveDash()
    {
        DisableDash = false;
        playerJump.canJump = true;
    }

    public void ActiveHit()
    {
        DisableHit = false;
    }

    private void Update()
    {
        anim.SetFloat("YVelocity", rb.velocity.y);


        if (falling)
            CoyoteTimeController();
    }

    void CoyoteTimeController()
    {
        if(coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("Grounded", false);
            playerJump.canJump = false;
            playerDash.canDash = false;
            canHit = false;
        }


    }

    private void OnEnable()
    {
        inputManager.slideUp += playerJump.Jump;
        inputManager.slideDown += playerDash.Dash;
        playerDash.OnDash += Dashing;
        playerDash.OnDashEnd += EndDashing;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerJump.Init(rb, anim);
        playerRun.Init(rb);
        playerDash.Init(playerCollider);
    }

    void Dashing()
    {
        if (!DisableDash && PauseMenu.isPaused== false)
        {
            playerDash.canDash = false;
            anim.SetTrigger("Dash");
            playerJump.canJump = false;
            canHit = false;
        }
    }

    void EndDashing()
    {
        if (!DisableJump)
            playerJump.canJump = true;
        anim.SetTrigger("EndDash");
        playerDash.canDash = true;
        canHit = true;
    }

    void RestoreParameters()
    {
        if (!DisableJump)
            playerJump.canJump = true;
        if(!DisableDash)
        playerDash.canDash = true;
        if(!DisableHit)
        canHit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && !playerDash.dashing)
        {
            falling = false;
            anim.SetBool("Grounded", true);
            if (!DisableJump)
                playerJump.canJump = true;
            if(!DisableDash)
                playerDash.canDash = true;
            canHit = true;
        }

        if (collision.gameObject.CompareTag("JumpObstacle"))
        {
            playerFail?.Invoke();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerFail?.Invoke();
        }

        if (collision.gameObject.CompareTag("Wall02"))
        {
            playerFail?.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && !playerDash.dashing)
        {
            falling = false;
            if (!DisableDash)
                playerDash.canDash = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && !playerDash.dashing)
        {
            coyoteTimer = coyoteTime;
            falling = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpObstacle"))
        {
            playerFail?.Invoke();
        }
    }




    private void OnDisable()
    {
        inputManager.slideUp -= playerJump.Jump;
        inputManager.slideDown -= playerDash.Dash;
    }

    public void PlayerDeath()
    {
        playerRun.enabled = false;
        rb.velocity = Vector3.zero;
        playerDash.canDash = false;
        playerJump.canJump = false;
        canHit = false;
        DisableDash = true;
        DisableJump = true;
        DisableHit = false;

    }

    public void PlayerHit()
    {
        if (canHit && !DisableHit && PauseMenu.isPaused==false)
        {
            hit.Hit();
            anim.SetTrigger("Punch");
            playerJump.canJump = false;
            playerDash.canDash = false;
            Invoke("RestoreParameters", 1f);
            playerPunch?.Invoke();
            canHit = false;
        }

    }
}
