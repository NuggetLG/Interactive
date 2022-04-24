using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public int numOfObj;
    [SerializeField][Range(0, 1)] public float Amp;
    [SerializeField][Range(-1, 1)] public float Num;

    private void Start()
    {
        for (int i = 0; i < numOfObj; i++)
        {
            Instantiate(prefab, transform);
        }
    }

    void Update()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            float x = i * 0.7f + Amp;
            float y = Mathf.Sin(Time.time + x);
            child.transform.localPosition = new Vector3(x, Num * y);
            i++;
        }
    }
}
