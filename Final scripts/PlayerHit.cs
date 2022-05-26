using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    float radius;

    [SerializeField]
    AudioClip soundEffect;


    public void Hit()
    {
        if(soundEffect != null)
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);

        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].CompareTag("Wall"))
            {
                collider[i].GetComponent<WallDestruction>().DestroyWall();
            }
            if (collider[i].CompareTag("Wall02"))
            {
                collider[i].GetComponent<WallTutorial>().DestroyWall();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
