using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    private Vector2 velocity;
    private void Update()
    {
        Vector2 worldMousePos = GetMousePos();
        Vector2 thisPosition = transform.position;
        Vector3 dir = (worldMousePos - thisPosition).normalized;

        velocity = dir * speed;

        Look(thisPosition + velocity);
    }

    private Vector4 GetMousePos()
    {
        Camera cam = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void Look(Vector2 targetPos)
    {
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 forward = targetPos - thisPosition;
        float radians = Mathf.Atan2(forward.y, forward.x) - Mathf.PI / 2;

        transform.rotation = Quaternion.Euler(0f, 0f, radians * Mathf.Rad2Deg);
    }
}
