using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrumeController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float yOffSet;
    [SerializeField] GameObject player;
    [SerializeField] float maxDistance;
    Rigidbody rb;

    public bool followY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        {
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);

        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (player.transform.position.y + yOffSet < transform.position.y)
            transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffSet, transform.position.z);

        if (followY)
            FollowYAxis();
    }

    void FollowYAxis()
    {
        if(player.transform.position.y + yOffSet > transform.position.y && !PlayerController.instance.falling)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffSet, transform.position.z);
        }
    }

    public void DeleteRestriction()
    {
        maxDistance = 0;
    }
}
