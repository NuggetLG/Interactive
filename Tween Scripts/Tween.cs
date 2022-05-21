using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    [SerializeField] private AnimationCurve Curve;

    [SerializeField] private float duration = 5;
    
    [SerializeField] private Vector3 startPos, endPos;

    public float Speed = 5f;

    private void Update()
    {
        var t = Time.time / duration;
        transform.position = Vector3.LerpUnclamped(startPos, endPos, Curve.Evaluate(t));
        transform.Rotate(0, 0, Speed);
    }

    private float EaseInOutCirc (float x)
    {
        return x < 0.5f ? (1 - Mathf.Sqrt(1f - Mathf.Pow(2 * x, 2))) / 2 :
            (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2, 2)) + 1) / 2;
    }
}
