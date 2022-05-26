using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDash : MonoBehaviour
{
    [HideInInspector]
    public bool canDash;
    public bool dashing;
    public Action OnDash;
    public Action OnDashEnd;

    CapsuleCollider capsuleCollider;
    Vector3 startingCenter;
    float startingHeight;
    public void Init(CapsuleCollider playerCollider)
    {
        capsuleCollider = playerCollider;
        startingCenter = capsuleCollider.center;
        startingHeight = capsuleCollider.height;
    }
    public void Dash()
    {
        if (canDash)
        {
            OnDash?.Invoke();
            dashing = true;
            capsuleCollider.center = new Vector3(startingCenter.x, 0.53f, 0);
            capsuleCollider.height = startingHeight / 2;
            Invoke("EndDash", 1.5f);
            canDash = false;
        }
    }

    public void EndDash()
    {
        OnDashEnd?.Invoke();
        dashing = false;
        capsuleCollider.center = startingCenter;
        capsuleCollider.height = startingHeight;
    }
}
