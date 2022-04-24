using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    [SerializeField] private float radius, theta;
    [SerializeField] private GameObject target;
    [SerializeField] private float radialSpeed, angularSpeed;

    [SerializeField] private float maxDistance, minDistance;

    private void Update()
    {
        theta += angularSpeed + Time.deltaTime;
        radius += radialSpeed * Time.deltaTime;

        Vector3 cartesian = PolarToCartesian(radius, theta);

        target.transform.position = cartesian;

        if (radius > maxDistance || radius < -maxDistance)
        {
            radialSpeed = radialSpeed * -1;
        }
    }

    private Vector3 PolarToCartesian(float radius, float theta)
    {
        return new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
    }
}
