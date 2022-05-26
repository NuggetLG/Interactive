using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    Rigidbody rb;
    Animator anim;
    [HideInInspector]
    public bool canJump;
    /// <summary>
    /// Initialize parameters for PlayerJump
    /// </summary>
    /// <param name="PlayerRb"></param>
    public void Init(Rigidbody PlayerRb, Animator PlayerAnim)
    {
        rb = PlayerRb;
        anim = PlayerAnim;
    }

    public void Jump()
    {
        if (canJump && PauseMenu.isPaused==false)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("Grounded", false);
            canJump = false;
        }
    }
}
