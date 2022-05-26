using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;

    /// <summary>
    /// Initialize parameters for PlayerRun
    /// </summary>
    /// <param name="PlayerRb"></param>
    public void Init(Rigidbody PlayerRb)
    {
        rb = PlayerRb;
    }

    public void FixedUpdate()
    {
        if(transform.parent == null)
        {
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);
        }

    }
}
